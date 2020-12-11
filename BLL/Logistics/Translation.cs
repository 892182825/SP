using System;
using System.Data;
using System.Data.SqlClient;


namespace BLL
{
    /// <summary>
    /// Translation 多语言翻译
    /// </summary>
    public class Translation
    {
        public Translation()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        private static string GetPath(string path, int a)
        {
            if (a > 15) return path; //防止死循环 
            if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(path)) == true)
            {
                return path;
            }
            else
            {
                return GetPath("../" + path, a + 1);
            }
        }

        private static string GetLanguageCode()
        {// Session["languageCode"] = sdr["languageCode"].ToString().Trim ();  
            if (System.Web.HttpContext.Current.Session["languageCode"] == null)
            {
                BLL.CommonClass .Transforms.JSExec("<script>parent.navigate('" + GetPath("Logout.aspx", 1) + "');</script>");
                System.Web.HttpContext.Current.Response.End();
            }
            return System.Web.HttpContext.Current.Session["languageCode"].ToString();
        }
        /// <summary>
        /// 获取文言代码
        /// </summary>
        /// <returns></returns>
        private static int GetLanguageID()
        {
            if (System.Web.HttpContext.Current.Session["LanguageID"] == null)
            {
                BLL.CommonClass.Transforms.JSExec("<script>parent.navigate('" + GetPath("Logout.aspx", 1) + "');</script>");
                System.Web.HttpContext.Current.Response.End();
            }
            return Convert.ToInt32(System.Web.HttpContext.Current.Session["LanguageID"]);
        }

        public static string Translate(string keyCode)
        {
            string tlText = string.Empty;
            int translateType = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["TranslateType"]);
            if (translateType == 1)
            {
                tlText = TranslateFromDB(keyCode);
            }
            else if (translateType == 2)
            {
                tlText = TranslateFromGlobal(keyCode);
            }
            else if (translateType == 3)
            {
                tlText = TranslateFromXML(keyCode);
            }
            return tlText;
        }
       // Translate
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyCode">在翻译词典中的键值</param>
        /// <param name="defaultText">默认值</param>
        /// <returns></returns>
        public static string Translate(string keyCode, string defaultText)
        {
            string tlText = string.Empty;
            int translateType = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["TranslateType"]);
            if (translateType == 1)
            {
                tlText = TranslateFromDB(keyCode, defaultText);
            }
            else if (translateType == 2)
            {
                tlText = TranslateFromGlobal(keyCode, defaultText);
            }
            else if (translateType == 3)
            {
                tlText = TranslateFromXML(keyCode, defaultText);
            }
            return tlText;
        }


        /// <summary>
        /// 从XML中读取翻译内容
        /// </summary>
        /// <param name="keyCode">键值</param>
        /// <returns></returns>
        public static string TranslateFromXML(string keyCode)
        {
            if (keyCode == "000000")
            {
                return "";
            }
            object objText = string .Empty ;
            try
            {
                string ExSql = string.Empty;
                objText =BLL.FileOperateBLL .ForXML .ReadLanguageByKeyCode(keyCode);
                if (objText != null)
                {
                    return objText.ToString();
                }
                else
                {
                    ExSql = "select t." + System.Web.HttpContext.Current.Session["languageCode"] + " from T_translation t where keyCode=@num";
                    SqlParameter spa = new SqlParameter("@num",SqlDbType.Char,6);
                    spa.Value = keyCode;
                    objText = DAL.DBHelper.ExecuteScalar(ExSql,spa,CommandType.Text);
                    if (objText != DBNull.Value && objText != null)
                    {
                        return objText.ToString();
                    }
                }
                return objText.ToString();
            }
            catch (Exception ex)
            {
                BLL.CommonClass .Transforms.JSAlert("翻译出错：" +ex.Message);
            }
            return objText.ToString();

        }

        /// <summary>
        ///  从XML中读取翻译内容
        /// </summary>
        /// <param name="keyCode">键值</param>
        /// <param name="defaultText">默认值</param>
        /// <returns></returns>
        public static string TranslateFromXML(string keyCode, string defaultText)
        {
            object objText = string .Empty ;
            try
            {
                if (keyCode == "000000")
                    return defaultText;
                else
                {
                    string ExSql = string.Empty;
                    objText = BLL.FileOperateBLL.ForXML.ReadLanguageByKeyCode(keyCode);
                    if (objText != null)
                    {
                        return objText.ToString();
                    }
                    else
                    {
                        ExSql = "select t." + System.Web.HttpContext.Current.Session["languageCode"] + " from T_translation t where keyCode=@num";
                        SqlParameter spa = new SqlParameter("@num", SqlDbType.Char, 6);
                        spa.Value = keyCode;
                        objText = DAL.DBHelper.ExecuteScalar(ExSql,spa,CommandType.Text);
                        if (objText != DBNull.Value && objText != null)
                        {
                            return objText.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                BLL.CommonClass.Transforms.JSAlert("翻译出错：" + ex.Message);
            }
            return objText.ToString();

        }

        #region 翻译取值 从全局变量中取值，适合多处出现并且经常被访问的页面
        /// <summary>
        /// 从全局变量中取翻译，没取到从数据库中取
        /// </summary>
        /// <param name="keyCode">在翻译词典中的键值</param>
        /// <returns></returns>
        public static string TranslateFromGlobal(string keyCode)
        {
            string tlText = string.Empty;
            string ExSql = string.Empty;
            int languageID = GetLanguageID();
            try
            {
                if (keyCode == "000000")
                    return "";
                System.Collections.Hashtable htb;
                if (System.Web.HttpContext.Current.Application["Tran_" + languageID] == null)
                    htb = new System.Collections.Hashtable();
                else
                    htb = (System.Collections.Hashtable)System.Web.HttpContext.Current.Application["Tran_" + languageID];
                if (htb.Contains(keyCode))
                {//己存在
                    tlText = htb[keyCode].ToString().Trim();
                }
                else
                {
                    ExSql = "select t." + System.Web.HttpContext.Current.Session["languageCode"] + " from T_translation t where keyCode=@num";
                    SqlParameter spa = new SqlParameter("@num", SqlDbType.Char, 6);
                    spa.Value = keyCode;
                    object objText = DAL.DBHelper.ExecuteScalar(ExSql, spa, CommandType.Text);
                    if (objText != DBNull.Value && objText != null)
                    {
                        tlText = objText.ToString();
                        htb.Add(keyCode, tlText);
                        System.Web.HttpContext.Current.Application["Tran_" + languageID] = htb;
                    }
                }
            }
            catch (Exception ex)
            {                
                BLL.CommonClass.Transforms.JSAlert("翻译出错：" + ex.Message);
            }
            return tlText;
        }


        /// <summary>
        /// 从全局变量中取翻译，没取到从数据库中取
        /// </summary>
        /// <param name="keyCode">在翻译词典中的键值</param>
        /// <param name="defaultText">默认值</param>
        /// <returns></returns>
        public static string TranslateFromGlobal(string keyCode, string defaultText)
        {
            string tlText = string.Empty;
            string ExSql = string.Empty;
            int languageID = GetLanguageID();
            try
            {
                if (keyCode == "000000")
                    return defaultText;
                System.Collections.Hashtable htb;
                if (System.Web.HttpContext.Current.Application["Tran_" + languageID] == null)
                    htb = new System.Collections.Hashtable();
                else
                    htb = (System.Collections.Hashtable)System.Web.HttpContext.Current.Application["Tran_" + languageID];
                if (htb.Contains(keyCode))
                {//己存在
                    tlText = htb[keyCode].ToString().Trim();
                }
                else
                {
                    ExSql = "select t." + System.Web.HttpContext.Current.Session["languageCode"] + " from T_translation t where keyCode=@num";
                    SqlParameter spa = new SqlParameter("@num", SqlDbType.Char, 6);
                    spa.Value = keyCode;
                    Object objText = DAL.DBHelper.ExecuteScalar(ExSql, spa, CommandType.Text);
                    if (objText != DBNull.Value && objText != null)
                    {
                        tlText = objText.ToString();
                        htb.Add(keyCode, tlText);
                        System.Web.HttpContext.Current.Application["Tran_" + languageID] = htb;
                    }
                }
                if (tlText == "")
                {
                    tlText = defaultText;
                }

            }
            catch (Exception ex)
            {
                BLL.CommonClass.Transforms.JSAlert("翻译出错：" + ex.Message);
            }
            return tlText;
        }
        #endregion 



        #region 翻译取值 从数据库中取值，适合很少出现并且很少被访问的页面
        /// <summary>
        /// 直接从数据库中取翻译值
        /// </summary>
        /// <param name="keyCode"></param>
        /// <returns></returns>
        public static string TranslateFromDB(string keyCode)
        {
            string tlText = string.Empty;
            string ExSql = string.Empty;
            int languageID = GetLanguageID();
            try
            {
                if (keyCode == "000000")
                    return "";
                ExSql = "select t." + System.Web.HttpContext.Current.Session["languageCode"] + " from T_translation t where keyCode=@num";
                SqlParameter spa = new SqlParameter("@num", SqlDbType.Char, 6);
                spa.Value = keyCode;
                object objText = DAL.DBHelper.ExecuteScalar(ExSql, spa, CommandType.Text);
                if (objText != DBNull.Value && objText != null)
                {
                    tlText = objText.ToString();                   
                }
            }
            catch (Exception ex)
            {
                BLL.CommonClass.Transforms.JSAlert("翻译出错：" + ex.Message);
            }
            return tlText;
        }
        /// <summary>
        /// 直接从数据库中取翻译值
        /// </summary>
        /// <param name="keyCode"></param>
        /// <returns></returns>
        public static string TranslateFromDB(string keyCode, string defaultText)
        {
            string tlText = string.Empty;
            string ExSql = string.Empty;
            int languageID = GetLanguageID();
            try
            {
                if (keyCode == "000000")
                    return defaultText;
                ExSql = "select t." + System.Web.HttpContext.Current.Session["languageCode"] + " from T_translation t where keyCode=@num";
                SqlParameter spa = new SqlParameter("@num", SqlDbType.Char, 6);
                spa.Value = keyCode;
                object objText = DAL.DBHelper.ExecuteScalar(ExSql, spa, CommandType.Text);
                if (objText != DBNull.Value && objText != null)
                {
                    tlText = objText.ToString();                   
                }
            }
            catch (Exception ex)
            {
                BLL.CommonClass.Transforms.JSAlert("翻译出错：" + ex.Message);
            }
            if (tlText == "")
                tlText = defaultText;
            return tlText;
        }

        #endregion 

        #region 控件翻译 从全局变量中取值，适合多处出现并且经常被访问的页面
        /// <summary>
        /// DataGrid控件的翻译
        /// </summary>
        /// <param name="dg">要翻译的DataGrid控件</param>
        /// <param name="columns">二维数组型参数，第二维第一个为字典键值，第二个为默认值</param>
        public static void TranslationDataGrid(System.Web.UI.WebControls.DataGrid dg, string[][] columns)
        {
            if (dg.HasControls())
            {
                int HeaderIndex = (((System.Web.UI.WebControls.DataGridItem)dg.Controls[0].Controls[0]).ItemType == System.Web.UI.WebControls.ListItemType.Header) ? 0 : 1;
                string tlText = string.Empty;
                for (int i = 0; i < dg.Columns.Count; i++)
                {
                    tlText = Translate(columns[i][0]);
                    if (tlText == "")
                        tlText = columns[i][1];
                    ((System.Web.UI.WebControls.TableCell)dg.Controls[0].Controls[HeaderIndex].Controls[i]).Text = tlText;
                }
            }
        }

        /// <summary>
        /// GridView控件的翻译
        /// </summary>
        /// <param name="dg">要翻译的DataGrid控件</param>
        /// <param name="columns">二维数组型参数，第二维第一个为字典键值，第二个为默认值</param>
        public static void TranslationGridView(System.Web.UI.WebControls.GridView gv, string[][] columns)
        {
            if (gv.HasControls())
            {
                if (((System.Web.UI.WebControls.GridViewRow)gv.Controls[0].Controls[0]).RowType != System.Web.UI.WebControls.DataControlRowType.EmptyDataRow)
                {
                    int HeaderIndex = (((System.Web.UI.WebControls.GridViewRow)gv.Controls[0].Controls[0]).RowType == System.Web.UI.WebControls.DataControlRowType.Header) ? 0 : 1;
                    // HeaderIndex = 0;
                    string tlText = string.Empty;
                    for (int i = 0; i < gv.Columns.Count; i++)
                    {
                        tlText = Translate(columns[i][0]);
                        if (tlText == "")
                            tlText = columns[i][1];
                        ((System.Web.UI.WebControls.TableCell)gv.Controls[0].Controls[HeaderIndex].Controls[i]).Text = tlText;
                    }
                }
            }
        }


        public static void TranslationGridView(System.Web.UI.WebControls.GridView gv, string[][] columns,bool IsTrem)
        {
            if (gv.HasControls())
            {
                if (((System.Web.UI.WebControls.GridViewRow)gv.Controls[0].Controls[0]).RowType != System.Web.UI.WebControls.DataControlRowType.EmptyDataRow)
                {
                    int HeaderIndex = (((System.Web.UI.WebControls.GridViewRow)gv.Controls[0].Controls[0]).RowType == System.Web.UI.WebControls.DataControlRowType.Header) ? 0 : 1;
                    // HeaderIndex = 0;
                    string tlText = string.Empty;
                    for (int i = 1; i < gv.Columns.Count-1; i++)
                    {
                        tlText = Translate(columns[i][0]);
                        if (tlText == "")
                            tlText = columns[i][1];
                        ((System.Web.UI.WebControls.TableCell)gv.Controls[0].Controls[HeaderIndex].Controls[i]).Text = tlText;
                    }
                }
            }
        }
        /// <summary>
        /// RadioButtonList控件翻译
        /// </summary>
        /// <param name="button">要翻译的服务器控件</param>
        /// <param name="columns">二维数组型参数，第二维第一个为字典键值，第二个为默认值</param>
        public static void TranslationRadioButtonList(System.Web.UI.WebControls.RadioButtonList rbl, string[][] items)
        {
            string tlText = string.Empty;
            for (int i = 0; i < items.Length; i++)
            {
                tlText = Translate(items[i][0]);
                if (tlText == "")
                    tlText = items[i][1];
                rbl.Items[i].Text = tlText;
            }

        }
        /// <summary>
        /// CheckBoxList控件翻译
        /// </summary>
        /// <param name="button">要翻译的服务器控件</param>
        /// <param name="columns">二维数组型参数，第二维第一个为字典键值，第二个为默认值</param>
        public static void TranslationCheckBoxList(System.Web.UI.WebControls.CheckBoxList chkl, string[][] items)
        {
            string tlText = string.Empty;
            for (int i = 0; i < items.Length; i++)
            {
                tlText = Translate(items[i][0]);
                if (tlText == "")
                    tlText = items[i][1];
                chkl.Items[i].Text = tlText;
            }
        }
        /// <summary>
        /// DropDownList控件翻译
        /// </summary>
        /// <param name="button">要翻译的服务器控件</param>
        /// <param name="keyCode">字典键值</param>
        /// <param name="columns">二维数组型参数，第二维第一个为字典键值，第二个为默认值</param>
        public static void TranslationDropDownList(System.Web.UI.WebControls.DropDownList ddl, string[][] items)
        {
            string tlText = string.Empty;
            for (int i = 0; i < items.Length; i++)
            {
                tlText = Translate(items[i][0]);
                if (tlText == "")
                    tlText = items[i][1];
                ddl.Items[i].Text = tlText;
            }
        }


        /// <summary>
        /// Button控件翻译
        /// </summary>
        /// <param name="button">要翻译的服务器控件</param>
        /// <param name="keyCode">字典键值</param>
        /// <param name="defaultText">默认值</param>
        public static void TranslationButton(System.Web.UI.WebControls.Button button, string keyCode, string defaultText)
        {
            string tlText = string.Empty;
            tlText = Translate(keyCode);
            if (tlText == "")
                tlText = defaultText;
            button.Text = tlText;
        }


        /// <summary>
        /// LinkButton控件翻译
        /// </summary>
        /// <param name="button">要翻译的服务器控件</param>
        /// <param name="keyCode">字典键值</param>
        /// <param name="defaultText">默认值</param>
        public static void TranslationLinkButton(System.Web.UI.WebControls.LinkButton button, string keyCode, string defaultText)
        {
            string tlText = string.Empty;
            tlText = Translate(keyCode);
            if (tlText == "")
                tlText = defaultText;
            button.Text = tlText;
        }

        /// <summary>
        /// ImageButton控件翻译
        /// </summary>
        /// <param name="button">要翻译的服务器控件</param>
        /// <param name="keyCode">字典键值</param>
        /// <param name="defaultText">默认值</param>
        public static void TranslationImageButton(System.Web.UI.WebControls.ImageButton button, string keyCode, string defaultText)
        {
            string tlText = string.Empty;
            tlText = Translate(keyCode);
            if (tlText == "")
                tlText = defaultText;
            button.AlternateText = tlText;
        }

        /// <summary>
        /// HyperLink控件翻译
        /// </summary>
        /// <param name="button">要翻译的服务器控件</param>
        /// <param name="keyCode">字典键值</param>
        /// <param name="defaultText">默认值</param>
        public static void TranslationHyperLink(System.Web.UI.WebControls.HyperLink button, string keyCode, string defaultText)
        {
            string tlText = string.Empty;
            tlText = Translate(keyCode);
            if (tlText == "")
                tlText = defaultText;
            button.Text = tlText;
        }

        /// <summary>
        /// Label控件翻译
        /// </summary>
        /// <param name="button">要翻译的服务器控件</param>
        /// <param name="keyCode">字典键值</param>
        /// <param name="defaultText">默认值</param>
        public static void TranslationLabel(System.Web.UI.WebControls.Label label, string keyCode, string defaultText)
        {
            string tlText = string.Empty;
            tlText = Translate(keyCode);
            if (tlText == "")
                tlText = defaultText;
            label.Text = tlText;
        }

        /// <summary>
        /// CheckBox控件翻译
        /// </summary>
        /// <param name="button">要翻译的服务器控件</param>
        /// <param name="keyCode">字典键值</param>
        /// <param name="defaultText">默认值</param>
        public static void TranslationCheckBox(System.Web.UI.WebControls.CheckBox checkBox, string keyCode, string defaultText)
        {
            string tlText = string.Empty;
            tlText = Translate(keyCode);
            if (tlText == "")
                tlText = defaultText;
            checkBox.Text = tlText;
        }

        /// <summary>
        /// RadioButton控件翻译
        /// </summary>
        /// <param name="button">要翻译的服务器控件</param>
        /// <param name="keyCode">字典键值</param>
        /// <param name="defaultText">默认值</param>
        public static void TranslationRadioButton(System.Web.UI.WebControls.RadioButton radioButton, string keyCode, string defaultText)
        {
            string tlText = string.Empty;
            tlText = Translate(keyCode);
            if (tlText == "")
                tlText = defaultText;
            radioButton.Text = tlText;
        }

        #region 验证控件翻译
        /// <summary>
        /// RequiredFieldValidator控件翻译
        /// </summary>
        /// <param name="button">要翻译的服务器控件</param>
        /// <param name="keyCode">字典键值</param>
        /// <param name="defaultText">默认值</param>
        public static void TranslationRequiredFieldValidator(System.Web.UI.WebControls.RequiredFieldValidator rfv, string keyCode, string defaultText)
        {
            string tlText = string.Empty;
            tlText = Translate(keyCode);
            if (tlText == "")
                tlText = defaultText;
            rfv.ErrorMessage = tlText;
        }
        /// <summary>
        /// RangeValidator控件翻译
        /// </summary>
        /// <param name="button">要翻译的服务器控件</param>
        /// <param name="keyCode">字典键值</param>
        /// <param name="defaultText">默认值</param>
        public static void TranslationRangeValidator(System.Web.UI.WebControls.RangeValidator rv, string keyCode, string defaultText)
        {
            string tlText = string.Empty;
            tlText = Translate(keyCode);
            if (tlText == "")
                tlText = defaultText;
            rv.ErrorMessage = tlText;
        }

        /// <summary>
        /// CompareValidator控件翻译
        /// </summary>
        /// <param name="button">要翻译的服务器控件</param>
        /// <param name="keyCode">字典键值</param>
        /// <param name="defaultText">默认值</param>
        public static void TranslationCompareValidator(System.Web.UI.WebControls.CompareValidator cv, string keyCode, string defaultText)
        {
            string tlText = string.Empty;
            tlText = Translate(keyCode);
            if (tlText == "")
                tlText = defaultText;
            cv.ErrorMessage = tlText;
        }

        /// <summary>
        /// RegularExpressionValidator控件翻译
        /// </summary>
        /// <param name="button">要翻译的服务器控件</param>
        /// <param name="keyCode">字典键值</param>
        /// <param name="defaultText">默认值</param>
        public static void TranslationRegularExpressionValidator(System.Web.UI.WebControls.RegularExpressionValidator re, string keyCode, string defaultText)
        {
            string tlText = string.Empty;
            tlText = Translate(keyCode);
            if (tlText == "")
                tlText = defaultText;
            re.Text = tlText;
        }

        #endregion

        #endregion

        #region 控件翻译 从数据库中取值，适合很少出现并且很少被访问的页面
        /// <summary>
        /// DataGrid控件的翻译，从数据库中取值
        /// </summary>
        /// <param name="dg">要翻译的DataGrid控件</param>
        /// <param name="columns">二维数组型参数，第二维第一个为字典键值，第二个为默认值</param>
        public static void TranslationDataGridDB(System.Web.UI.WebControls.DataGrid dg, string[][] columns)
        {
            if (dg.HasControls())
            {

                int HeaderIndex = (((System.Web.UI.WebControls.DataGridItem)dg.Controls[0].Controls[0]).ItemType == System.Web.UI.WebControls.ListItemType.Header) ? 0 : 1;
                string tlText = string.Empty;
                for (int i = 0; i < dg.Columns.Count; i++)
                {
                    tlText = Translate(columns[i][0]);
                    if (tlText == "")
                        tlText = columns[i][1];
                    ((System.Web.UI.WebControls.TableCell)dg.Controls[0].Controls[HeaderIndex].Controls[i]).Text = tlText;
                }
            }
        }

        /// <summary>
        /// GridView控件的翻译，从数据库中取值
        /// </summary>
        /// <param name="dg">要翻译的DataGrid控件</param>
        /// <param name="columns">二维数组型参数，第二维第一个为字典键值，第二个为默认值</param>
        public static void TranslationGridViewDB(System.Web.UI.WebControls.GridView gv, string[][] columns)
        {
            if (gv.HasControls())
            {
                if (((System.Web.UI.WebControls.GridViewRow)gv.Controls[0].Controls[0]).RowType != System.Web.UI.WebControls.DataControlRowType.EmptyDataRow)
                {
                    int HeaderIndex = (((System.Web.UI.WebControls.GridViewRow)gv.Controls[0].Controls[0]).RowType == System.Web.UI.WebControls.DataControlRowType.Header) ? 0 : 1;
                    string tlText = string.Empty;
                    for (int i = 0; i < gv.Columns.Count; i++)
                    {
                        tlText = Translate(columns[i][0]);
                        if (tlText == "")
                            tlText = columns[i][1];
                        ((System.Web.UI.WebControls.TableCell)gv.Controls[0].Controls[HeaderIndex].Controls[i]).Text = tlText;
                    }
                }
            }
        }
        /// <summary>
        /// RadioButtonList控件翻译，从数据库中取值
        /// </summary>
        /// <param name="button">要翻译的服务器控件</param>
        /// <param name="columns">二维数组型参数，第二维第一个为字典键值，第二个为默认值</param>
        public static void TranslationRadioButtonListDB(System.Web.UI.WebControls.RadioButtonList rbl, string[][] items)
        {
            string tlText = string.Empty;
            for (int i = 0; i < items.Length; i++)
            {
                tlText = Translate(items[i][0]);
                if (tlText == "")
                    tlText = items[i][1];
                rbl.Items[i].Text = tlText;
            }

        }
        /// <summary>
        /// CheckBoxList控件翻译，从数据库中取值
        /// </summary>
        /// <param name="button">要翻译的服务器控件</param>
        /// <param name="columns">二维数组型参数，第二维第一个为字典键值，第二个为默认值</param>
        public static void TranslationCheckBoxListDB(System.Web.UI.WebControls.CheckBoxList chkl, string[][] items)
        {
            string tlText = string.Empty;
            for (int i = 0; i < items.Length; i++)
            {
                tlText = Translate(items[i][0]);
                if (tlText == "")
                    tlText = items[i][1];
                chkl.Items[i].Text = tlText;
            }
        }
        /// <summary>
        /// DropDownList控件翻译，从数据库中取值
        /// </summary>
        /// <param name="button">要翻译的服务器控件</param>
        /// <param name="keyCode">字典键值</param>
        /// <param name="columns">二维数组型参数，第二维第一个为字典键值，第二个为默认值</param>
        public static void TranslationDropDownListDB(System.Web.UI.WebControls.DropDownList ddl, string[][] items)
        {
            string tlText = string.Empty;
            for (int i = 0; i < items.Length; i++)
            {
                tlText = Translate(items[i][0]);
                if (tlText == "")
                    tlText = items[i][1];
                ddl.Items[i].Text = tlText;
            }
        }


        /// <summary>
        /// Button控件翻译，从数据库中取值
        /// </summary>
        /// <param name="button">要翻译的服务器控件</param>
        /// <param name="keyCode">字典键值</param>
        /// <param name="defaultText">默认值</param>
        public static void TranslationButtonDB(System.Web.UI.WebControls.Button button, string keyCode, string defaultText)
        {
            string tlText = string.Empty;
            tlText = Translate(keyCode);
            if (tlText == "")
                tlText = defaultText;
            button.Text = tlText;
        }


        /// <summary>
        /// LinkButton控件翻译，从数据库中取值
        /// </summary>
        /// <param name="button">要翻译的服务器控件</param>
        /// <param name="keyCode">字典键值</param>
        /// <param name="defaultText">默认值</param>
        public static void TranslationLinkButtonDB(System.Web.UI.WebControls.LinkButton button, string keyCode, string defaultText)
        {
            string tlText = string.Empty;
            tlText = Translate(keyCode);
            if (tlText == "")
                tlText = defaultText;
            button.Text = tlText;
        }

        /// <summary>
        /// ImageButton控件翻译，从数据库中取值
        /// </summary>
        /// <param name="button">要翻译的服务器控件</param>
        /// <param name="keyCode">字典键值</param>
        /// <param name="defaultText">默认值</param>
        public static void TranslationImageButtonDB(System.Web.UI.WebControls.ImageButton button, string keyCode, string defaultText)
        {
            string tlText = string.Empty;
            tlText = Translate(keyCode);
            if (tlText == "")
                tlText = defaultText;
            button.AlternateText = tlText;
        }

        /// <summary>
        /// HyperLink控件翻译，从数据库中取值
        /// </summary>
        /// <param name="button">要翻译的服务器控件</param>
        /// <param name="keyCode">字典键值</param>
        /// <param name="defaultText">默认值</param>
        public static void TranslationHyperLinkDB(System.Web.UI.WebControls.HyperLink button, string keyCode, string defaultText)
        {
            string tlText = string.Empty;
            tlText = Translate(keyCode);
            if (tlText == "")
                tlText = defaultText;
            button.Text = tlText;
        }

        /// <summary>
        /// Label控件翻译，从数据库中取值
        /// </summary>
        /// <param name="button">要翻译的服务器控件</param>
        /// <param name="keyCode">字典键值</param>
        /// <param name="defaultText">默认值</param>
        public static void TranslationLabelDB(System.Web.UI.WebControls.Label label, string keyCode, string defaultText)
        {
            string tlText = string.Empty;
            tlText = Translate(keyCode);
            if (tlText == "")
                tlText = defaultText;
            label.Text = tlText;
        }

        /// <summary>
        /// CheckBox控件翻译，从数据库中取值
        /// </summary>
        /// <param name="button">要翻译的服务器控件</param>
        /// <param name="keyCode">字典键值</param>
        /// <param name="defaultText">默认值</param>
        public static void TranslationCheckBoxDB(System.Web.UI.WebControls.CheckBox checkBox, string keyCode, string defaultText)
        {
            string tlText = string.Empty;
            tlText = Translate(keyCode);
            if (tlText == "")
                tlText = defaultText;
            checkBox.Text = tlText;
        }

        /// <summary>
        /// RadioButton控件翻译，从数据库中取值
        /// </summary>
        /// <param name="button">要翻译的服务器控件</param>
        /// <param name="keyCode">字典键值</param>
        /// <param name="defaultText">默认值</param>
        public static void TranslationRadioButtonDB(System.Web.UI.WebControls.RadioButton radioButton, string keyCode, string defaultText)
        {
            string tlText = string.Empty;
            tlText = Translate(keyCode);
            if (tlText == "")
                tlText = defaultText;
            radioButton.Text = tlText;
        }

        #region 验证控件翻译
        /// <summary>
        /// RequiredFieldValidator控件翻译，从数据库中取值
        /// </summary>
        /// <param name="button">要翻译的服务器控件</param>
        /// <param name="keyCode">字典键值</param>
        /// <param name="defaultText">默认值</param>
        public static void TranslationRequiredFieldValidatorDB(System.Web.UI.WebControls.RequiredFieldValidator rfv, string keyCode, string defaultText)
        {
            string tlText = string.Empty;
            tlText = Translate(keyCode);
            if (tlText == "")
                tlText = defaultText;
            rfv.ErrorMessage = tlText;
        }
        /// <summary>
        /// RangeValidator控件翻译，从数据库中取值
        /// </summary>
        /// <param name="button">要翻译的服务器控件</param>
        /// <param name="keyCode">字典键值</param>
        /// <param name="defaultText">默认值</param>
        public static void TranslationRangeValidatorDB(System.Web.UI.WebControls.RangeValidator rv, string keyCode, string defaultText)
        {
            string tlText = string.Empty;
            tlText = Translate(keyCode);
            if (tlText == "")
                tlText = defaultText;
            rv.ErrorMessage = tlText;
        }

        /// <summary>
        /// CompareValidator控件翻译，从数据库中取值
        /// </summary>
        /// <param name="button">要翻译的服务器控件</param>
        /// <param name="keyCode">字典键值</param>
        /// <param name="defaultText">默认值</param>
        public static void TranslationCompareValidatorDB(System.Web.UI.WebControls.CompareValidator cv, string keyCode, string defaultText)
        {
            string tlText = string.Empty;
            tlText = Translate(keyCode);
            if (tlText == "")
                tlText = defaultText;
            cv.ErrorMessage = tlText;
        }

        /// <summary>
        /// RegularExpressionValidator控件翻译，从数据库中取值
        /// </summary>
        /// <param name="button">要翻译的服务器控件</param>
        /// <param name="keyCode">字典键值</param>
        /// <param name="defaultText">默认值</param>
        public static void TranslationRegularExpressionValidatorDB(System.Web.UI.WebControls.RegularExpressionValidator re, string keyCode, string defaultText)
        {
            string tlText = string.Empty;
            tlText = Translate(keyCode);
            if (tlText == "")
                tlText = defaultText;
            re.Text = tlText;
        }

        #endregion
        #endregion

    }


}
