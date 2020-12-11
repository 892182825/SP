using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.other.Company;
using System.Collections;
using System.Web;
using Model.Other;


namespace BLL
{
    public class TranslationBase : System.Web.UI.Page
    {
        //统一判断权限
        protected void TranslationBase_Load(object sender, EventArgs e)
        {
            //获取请求地址url
            string url = Request.Url.ToString();
            //获取页面名称
            string pageName = url.Substring(url.LastIndexOf('/') + 1).ToLower();
            //判断是否带参数了
            int spcialIndex = pageName.IndexOf('?');
            if (spcialIndex != -1)
                pageName = pageName.Substring(0, spcialIndex).ToLower();

            if (pageName != "index.aspx" && pageName != "zh-cn.aspx" && pageName != "refurbish.aspx" && pageName != "findpass.aspx" && pageName != "pfindpass.aspx"
                && pageName != "findpass1.aspx" && pageName != "checkadv1.aspx" && pageName != "zhifu.aspx" && pageName != "shdl.aspx")
            {
                string type = IsChaoShi("");
                if (type != "")
                {
                    Response.Redirect("~/ReFurbish.aspx?type=" + type);
                }
            }
        }
        public string IsChaoShi(string str)
        {
            string type = "0";
            if (Convert.ToDateTime(Session["ReFurbish_Timeout"]) < DateTime.Now)
            {
                type = "1";
                return type;
            }

            if (Session["Company"] == null && Session["Store"] == null && Session["Member"] == null && Session["Branch"] == null)
            {
                type = "1";
                return type;
            }
            string manageID = "";
            if (Application["jinzhi"] != null)
            {
                if (Session["Branch"] != null && (!BlackListBLL.GetSystem("B")))
                {
                    type = "2";
                    return type; //登陆设置 分公司登陆 退出
                }

                if (Session["Member"] != null && (!BlackListBLL.GetSystem("H")))
                {
                    type = "2";
                    return type;  //登陆设置 会员登陆 退出
                }
                if (Session["Membermobile"] != null && (!BlackListBLL.GetSystem("H")))
                {
                    type = "3";
                    return type;
                }

                if (Session["Store"] != null && (!BlackListBLL.GetSystem("D")))
                {
                    type = "2";
                    return type;  //登陆设置 店铺登陆 退出
                }

                manageID = BLL.CommonClass.CommonDataBLL.getManageID(1);

                if (Session["Company"] != null && Session["Company"].ToString() != manageID && (!BlackListBLL.GetSystem("G")))
                {
                    type = "2";
                    return type;  //登陆设置 管理员退出  除了''
                }

                if (Session["Company"] != null && Session["permission"] != null && Application["jinzhi"].ToString().IndexOf("J") >= 0) // 'J'是结算时的状态
                {
                    Hashtable table = (Hashtable)HttpContext.Current.Session["permission"];
                    if (!table.Contains(EnumCompanyPermission.FinanceJiesuan))
                    {
                        type = "1";
                        return type;  //结算时 没有结算权限的管理员退出					
                    }
                }
                
            }

            ////会员被注销时，自动退出系统 
            //if (Session["Member"] != null)
            //{
            //    if (DAL.CommonDataDAL.GetIsActive(Session["Member"].ToString()))
            //    {
            //        type = "1";
            //        return type;
            //    }
            //}

            string bianhao = "";
            int UserType = -1;

            int loginType = 0;

            if (Session["Member"] != null)
            {
                bianhao = Session["Member"].ToString();
                loginType = 3;
                UserType = 0;
            }
            else if (Session["Store"] != null)
            {
                bianhao = Session["Store"].ToString();
                loginType = 2;
                UserType = 4;
            }
            else if (Session["Company"] != null)
            {
                bianhao = Session["Company"].ToString();
                loginType = 1;
                UserType = 2;
            }
            else if (Session["Branch"] != null)
            {
                bianhao = Session["Branch"].ToString();
                UserType = 3;
            }


            manageID = BLL.CommonClass.CommonDataBLL.getManageID(loginType);

            // 黑名单处理 开始

            string[] SecPostion = Request.ServerVariables["REMOTE_ADDR"].ToString().Split('.');//客户IP地址

            //string ipAddress = SecPostion[0] + "." + SecPostion[1];
            string ipAddress = Request.ServerVariables["REMOTE_ADDR"].ToString();//客户IP地址
            try
            {
                if (bianhao != manageID && 0 < BlackListBLL.GetLikeIPCount(ipAddress))
                {
                    type = "2";
                    return type;
                }
            }
            catch
            {
                return "";
            }
            //限制区域登陆
            try
            {
                if (bianhao != manageID && BlackListBLL.GetLikeAddress(bianhao))
                {
                    type = "2";
                    return type;
                }
            }
            catch
            {
                return "";
            }

            if (bianhao == "" || UserType == -1) return "";
            try
            {
                if (0 < BlackListBLL.GetLikeIPCount(UserType, bianhao))
                {
                    type = "2";
                    return type;
                }
            }
            catch
            {
                return "";
            }
            // 黑名单处理 结束 	
            if (BLL.CommonClass.Login.isDenyLogin())
            //限时登陆
            {
                type = "2";
                return type;
            }

            return "";
        }
        /// <summary>
        /// 翻译基类构造函数
        /// </summary>
        public TranslationBase()
            : base()
        {
            this.Load += new EventHandler(TranslationBase_Load);
        }
        /// <summary>
        /// 从局变量中读取翻译值  [通过translation类和T_translation表翻译]
        /// </summary>
        /// <param name="keyCode"></param>
        /// <param name="defaultText"></param>
        /// <returns></returns>
        public  string GetTran(string keyCode, string defaultText)
        {
            return BLL.Translation.Translate(keyCode, defaultText);
        }
        public static string GetTran1(string keyCode, string defaultText)
        {
            return BLL.Translation.Translate(keyCode, defaultText);
        }


        /// <summary>
        /// 从局变量中读取翻译值 [通过translation类和T_translation表翻译]
        /// </summary>
        /// <param name="keyCode"></param>
        /// <returns></returns>
        public string GetTran(string keyCode)
        {
            return BLL.Translation.Translate(keyCode);
        }



        /// <summary>
        /// 服务器控件的翻译，从全局变量中取值
        /// </summary>
        /// <param name="control">要翻译的控件</param>
        /// <param name="items">二维数组型参数，第二维第一个为字典键值，第二个为默认值</param>
        public void TranControls(object control, string[][] items)
        {
            switch (control.GetType().ToString())	//
            {
                case "System.Web.UI.WebControls.GridView":
                    System.Web.UI.WebControls.GridView gv = (System.Web.UI.WebControls.GridView)control;
                    Translation.TranslationGridView(gv, items);
                    break;
                case "System.Web.UI.WebControls.DataGrid":
                    System.Web.UI.WebControls.DataGrid dg = (System.Web.UI.WebControls.DataGrid)control;
                    Translation.TranslationDataGrid(dg, items);
                    break;
                case "System.Web.UI.WebControls.RadioButtonList":
                    System.Web.UI.WebControls.RadioButtonList rbl = (System.Web.UI.WebControls.RadioButtonList)control;
                    Translation.TranslationRadioButtonList(rbl, items);
                    break;
                case "System.Web.UI.WebControls.DropDownList":
                    System.Web.UI.WebControls.DropDownList ddl = (System.Web.UI.WebControls.DropDownList)control;
                    Translation.TranslationDropDownList(ddl, items);
                    break;
                case "System.Web.UI.WebControls.CheckBoxList":
                    System.Web.UI.WebControls.CheckBoxList chkl = (System.Web.UI.WebControls.CheckBoxList)control;
                    Translation.TranslationCheckBoxList(chkl, items);
                    break;
                case "System.Web.UI.WebControls.Button":
                    System.Web.UI.WebControls.Button button = (System.Web.UI.WebControls.Button)control;
                    Translation.TranslationButton(button, items[0][0], items[0][1]);
                    break;
                case "System.Web.UI.WebControls.LinkButton":
                    System.Web.UI.WebControls.LinkButton lbtn = (System.Web.UI.WebControls.LinkButton)control;
                    Translation.TranslationLinkButton(lbtn, items[0][0], items[0][1]);
                    break;
                case "System.Web.UI.WebControls.ImageButton":
                    System.Web.UI.WebControls.ImageButton imgBtn = (System.Web.UI.WebControls.ImageButton)control;
                    Translation.TranslationImageButton(imgBtn, items[0][0], items[0][1]);
                    break;
                case "System.Web.UI.WebControls.HyperLink":
                    System.Web.UI.WebControls.HyperLink hl = (System.Web.UI.WebControls.HyperLink)control;
                    Translation.TranslationHyperLink(hl, items[0][0], items[0][1]);
                    break;
                case "System.Web.UI.WebControls.Label":
                    System.Web.UI.WebControls.Label label = (System.Web.UI.WebControls.Label)control;
                    Translation.TranslationLabel(label, items[0][0], items[0][1]);
                    break;
                case "System.Web.UI.WebControls.CheckBox":
                    System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)control;
                    Translation.TranslationCheckBox(chk, items[0][0], items[0][1]);
                    break;
                case "System.Web.UI.WebControls.RadioButton":
                    System.Web.UI.WebControls.RadioButton rbtn = (System.Web.UI.WebControls.RadioButton)control;
                    Translation.TranslationRadioButton(rbtn, items[0][0], items[0][1]);
                    break;
                case "System.Web.UI.WebControls.RequiredFieldValidator":
                    System.Web.UI.WebControls.RequiredFieldValidator rfv = (System.Web.UI.WebControls.RequiredFieldValidator)control;
                    Translation.TranslationRequiredFieldValidator(rfv, items[0][0], items[0][1]);
                    break;
                case "System.Web.UI.WebControls.RangeValidator":
                    System.Web.UI.WebControls.RangeValidator rv = (System.Web.UI.WebControls.RangeValidator)control;
                    Translation.TranslationRangeValidator(rv, items[0][0], items[0][1]);
                    break;
                case "System.Web.UI.WebControls.CompareValidator":
                    System.Web.UI.WebControls.CompareValidator cv = (System.Web.UI.WebControls.CompareValidator)control;
                    Translation.TranslationCompareValidator(cv, items[0][0], items[0][1]);
                    break;
                case "System.Web.UI.WebControls.RegularExpressionValidator":
                    System.Web.UI.WebControls.RegularExpressionValidator rev = (System.Web.UI.WebControls.RegularExpressionValidator)control;
                    Translation.TranslationRegularExpressionValidator(rev, items[0][0], items[0][1]);
                    break;
            }
        }


        public void TranControls1(object control, string[][] items)
        {
            System.Web.UI.WebControls.GridView gv = (System.Web.UI.WebControls.GridView)control;
            Translation.TranslationGridView(gv, items,true);
        }

        /// <summary>
        /// 服务器控件的翻译，从数据库中取值
        /// </summary>
        /// <param name="control">要翻译的控件</param>
        /// <param name="items">二维数组型参数，第二维第一个为字典键值，第二个为默认值</param>
        public void TranControlsDB(object control, string[][] items)
        {
            switch (control.GetType().ToString())	//
            {
                case "System.Web.UI.WebControls.GridView":
                    System.Web.UI.WebControls.GridView gv = (System.Web.UI.WebControls.GridView)control;
                    Translation.TranslationGridViewDB(gv, items);
                    break;
                case "System.Web.UI.WebControls.DataGrid":
                    System.Web.UI.WebControls.DataGrid  dg = (System.Web.UI.WebControls.DataGrid)control;
                    Translation.TranslationDataGridDB(dg, items);
                    break;
                case "System.Web.UI.WebControls.RadioButtonList":
                    System.Web.UI.WebControls.RadioButtonList rbl = (System.Web.UI.WebControls.RadioButtonList)control;
                    Translation.TranslationRadioButtonListDB(rbl, items);
                    break;
                case "System.Web.UI.WebControls.DropDownList":
                    System.Web.UI.WebControls.DropDownList ddl = (System.Web.UI.WebControls.DropDownList)control;
                    Translation.TranslationDropDownListDB(ddl, items);
                    break;
                case "System.Web.UI.WebControls.CheckBoxList":
                    System.Web.UI.WebControls.CheckBoxList chkl = (System.Web.UI.WebControls.CheckBoxList)control;
                    Translation.TranslationCheckBoxListDB(chkl, items);
                    break;
                case "System.Web.UI.WebControls.Button":
                    System.Web.UI.WebControls.Button button = (System.Web.UI.WebControls.Button)control;
                    Translation.TranslationButtonDB(button, items[0][0], items[0][1]);
                    break;
                case "System.Web.UI.WebControls.LinkButton":
                    System.Web.UI.WebControls.LinkButton lbtn = (System.Web.UI.WebControls.LinkButton)control;
                    Translation.TranslationLinkButtonDB(lbtn, items[0][0], items[0][1]);
                    break;
                case "System.Web.UI.WebControls.ImageButton":
                    System.Web.UI.WebControls.ImageButton imgBtn = (System.Web.UI.WebControls.ImageButton)control;
                    Translation.TranslationImageButtonDB(imgBtn, items[0][0], items[0][1]);
                    break;
                case "System.Web.UI.WebControls.HyperLink":
                    System.Web.UI.WebControls.HyperLink hl = (System.Web.UI.WebControls.HyperLink)control;
                    Translation.TranslationHyperLinkDB(hl, items[0][0], items[0][1]);
                    break;
                case "System.Web.UI.WebControls.Label":
                    System.Web.UI.WebControls.Label label = (System.Web.UI.WebControls.Label)control;
                    Translation.TranslationLabelDB(label, items[0][0], items[0][1]);
                    break;
                case "System.Web.UI.WebControls.CheckBox":
                    System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)control;
                    Translation.TranslationCheckBoxDB(chk, items[0][0], items[0][1]);
                    break;
                case "System.Web.UI.WebControls.RadioButton":
                    System.Web.UI.WebControls.RadioButton rbtn = (System.Web.UI.WebControls.RadioButton)control;
                    Translation.TranslationRadioButtonDB(rbtn, items[0][0], items[0][1]);
                    break;
                case "System.Web.UI.WebControls.RequiredFieldValidator":
                    System.Web.UI.WebControls.RequiredFieldValidator rfv = (System.Web.UI.WebControls.RequiredFieldValidator)control;
                    Translation.TranslationRequiredFieldValidatorDB(rfv, items[0][0], items[0][1]);
                    break;
                case "System.Web.UI.WebControls.RangeValidator":
                    System.Web.UI.WebControls.RangeValidator rv = (System.Web.UI.WebControls.RangeValidator)control;
                    Translation.TranslationRangeValidatorDB(rv, items[0][0], items[0][1]);
                    break;
                case "System.Web.UI.WebControls.CompareValidator":
                    System.Web.UI.WebControls.CompareValidator cv = (System.Web.UI.WebControls.CompareValidator)control;
                    Translation.TranslationCompareValidatorDB(cv, items[0][0], items[0][1]);
                    break;
                case "System.Web.UI.WebControls.RegularExpressionValidator":
                    System.Web.UI.WebControls.RegularExpressionValidator rev = (System.Web.UI.WebControls.RegularExpressionValidator)control;
                    Translation.TranslationRegularExpressionValidatorDB(rev, items[0][0], items[0][1]);
                    break;
            }
        }	
    }

}
