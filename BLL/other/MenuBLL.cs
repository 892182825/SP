using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using DAL;

namespace BLL.other
{
    public class MenuBLL
    {

        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="type">菜单类型</param>
        /// <param name="pid">菜单父菜单ID</param>
        /// <returns>返回菜单列表</returns>
        public static string GetMenuLeft(int pid,int type)
        {
            string returnStr = "";
            if (type == 0)
            {
                string bigMenuName =  MenuDAL.GetLeftMenuName(pid);
                string mName = bigMenuName;
                int a = bigMenuName.IndexOf(">");
                if (bigMenuName.Substring(a+1).Length > 15)
                {
                    mName = bigMenuName.Substring(0, a+1+15) + "...";
                }
                returnStr = "<b><font color=\"#005575\"><span title='" + bigMenuName.Substring(a+1) + "'>" + mName + "</span></font></b>";
            }
            else if (type == 1)
            {
                DataTable dt = MenuDAL.GetLeftMenuNameDT(pid);
                string menufile = "", menuName = "";
                returnStr = "<DIV STYLE='PADDING-LEFT:15px'>";
                int count = 0;
                string meName = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    menufile = dt.Rows[i]["menufile"].ToString();
                    menuName = dt.Rows[i]["menuName"].ToString();
                    meName = menuName;
                    if (menuName.Length > 15)
                    {
                        meName = menuName.Substring(0, 15) + "...";
                    }
                    if (i > 0)
                    {
                        returnStr += " <br>";
                    }
                    if (dt.Rows[i]["id"].ToString() == "84" || dt.Rows[i]["id"].ToString() == "90" || dt.Rows[i]["id"].ToString() == "130" || dt.Rows[i]["id"].ToString() == "131" || dt.Rows[i]["id"].ToString() == "132" || dt.Rows[i]["id"].ToString() == "133")
                    {
                        returnStr += "&nbsp;&nbsp;&nbsp;&nbsp;• &nbsp;<a class='white' name = 'aHref' id=" + pid.ToString() + count.ToString() + " onclick = \"alert('" + new TranslationBase().GetTran("005818", "暂不开放") + "！')\" title='" + menuName + "' target=mainframe>" + meName + "</a>";
                    }
                    else
                    {
                        returnStr += "&nbsp;&nbsp;&nbsp;&nbsp;• &nbsp;<a class='white' name = 'aHref' href=" + menufile + " id=" + pid.ToString() + count.ToString() + " onclick = 'Change(this.id)' title='" + menuName + "' target=mainframe>" + meName + "</a>";
                    }
                    count++;
                }
                returnStr += "</div>";

                //returnStr += "<script>function chang123(id){";
                //returnStr += "alert(id);";
                //count = 0;
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    returnStr += "menu" + pid.ToString() + count.ToString() + ".className='white';";
                //    count++;
                //}
                //returnStr += "switch(id)	{";
                //count = 0;
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    returnStr += "case " + pid.ToString() + count.ToString() + ":menu" + pid.ToString() + count.ToString() + ".className='yellow';break;";
                //    count++;
                //}
                //returnStr += "}}</script>";

            }
            return returnStr;
        }

        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="type">菜单类型</param>
        /// <param name="pid">菜单父菜单ID</param>
        /// <returns>返回菜单列表</returns>
        public static string GetMenu(int type, int pid)
        {
            IList<MenuModel> menus = MenuDAL.GetThisMenu(type, pid);
            StringBuilder str = new StringBuilder();
            int i = 0;
            int count = 0;
            if (GetMenuCount(type, pid) > 10)
            {
                str.Append("<table border='0' cellpadding='0' cellspacing='0'>");
                foreach (MenuModel menu in menus)
                {
                    string mName = menu.MenuName;
                    if (mName.Length > 15)
                    {
                        mName = mName.Substring(0, 15) + "...";
                    }

                    if (menu.Isfold == 1)
                    {
                        str.Append("<tr style='display:none'>");
                    }
                    else
                    {
                        str.Append("<tr>");
                    }

                    if (i == 0)
                    {
                        str.Append("<td style='padding-top:10px' >");
                        i++;
                    }
                    else
                    {
                        str.Append("<td >");
                    }

                    if (menu.ID == 87)
                    {
                        str.Append("<a  class='white' title='" + menu.MenuName + "' href='");
                        str.Append(menu.MenuFile);
                        str.Append("'id='");
                        str.Append("menu" + count);
                        str.Append("' onclick='chang(");
                        str.Append(count);
                        str.Append(");");
                        str.Append("'target='_parent' >");
                        str.Append("<img src='");
                        str.Append(menu.ImgFile);
                        str.Append("' border='0' align='absmiddle' />");
                        str.Append(mName);
                        str.Append("</a></td>");
                        str.Append("</tr>");
                    }
                    else
                    {
                        str.Append("<a  class='white' title='" + menu.MenuName + "' href='");
                        str.Append(menu.MenuFile);
                        str.Append("'id='");
                        str.Append("menu" + count);
                        str.Append("' onclick='chang(");
                        str.Append(count);
                        str.Append(");");
                        str.Append("' target='mainframe'>");
                        str.Append("<img src='");
                        str.Append(menu.ImgFile);
                        str.Append("' border='0' align='absmiddle' />");
                        str.Append(mName);
                        str.Append("</a></td>");
                        str.Append("</tr>");
                    }
                    count++;
                }
                str.Append("</table>");
            }
            else
            {
                str.Append("<table width='176' height='24' border='0' align='center' cellpadding='0' cellspacing='0'>");
                str.Append("<tr>");
                str.Append("<td align='center' background='images/lmenu.gif' class='lmenubti'>");
                str.Append(GetFormatTitle(GetTitleName(type, pid),10));
                str.Append("</td>");
                str.Append("</tr>");
                str.Append("</table>");
                str.Append("<table width='176px' height='330' class='modbox' border='0' align='center' cellpadding='0' cellspacing='0'><tr><td valign='top'>");
                str.Append("<table width='174px' border='0' align='center' cellpadding='0' cellspacing='0'>");

                foreach (MenuModel menu in menus)
                {
                    string mName = menu.MenuName;
                    if (mName.Length > 15)
                    {
                        mName = mName.Substring(0, 15) + "...";
                    }

                    if (menu.Isfold == 1)
                    {
                        str.Append("<tr style='display:none'>");
                    }
                    else
                    {
                        str.Append("<tr>");
                    }

                    if (i == 0)
                    {
                        str.Append("<td style='padding-top:10px' >");
                        i++;
                    }
                    else
                    {
                        str.Append("<td >");
                    }
                    if (menu.ID == 87)
                    {
                        str.Append("<a  class='white' title='" + menu.MenuName + "' href='");
                        str.Append(menu.MenuFile);
                        str.Append("'id='");
                        str.Append("menu" + count);
                        str.Append("' onclick='chang(");
                        str.Append(count);
                        str.Append(");");
                        str.Append("'target='_parent' >");
                        str.Append("<img src='");
                        str.Append(menu.ImgFile);
                        str.Append("' border='0' align='absmiddle' />");
                        str.Append(mName);
                        str.Append("</a></td>");
                        str.Append("</tr>");
                    }
                    else
                    {
                        str.Append("<a  class='white' title='" + menu.MenuName + "' href='");
                        str.Append(menu.MenuFile);
                        str.Append("'id='");
                        str.Append("menu" + count);
                        str.Append("' onclick='chang(");
                        str.Append(count);
                        str.Append(");");
                        str.Append("'target='mainframe' >");
                        str.Append("<img src='");
                        str.Append(menu.ImgFile);
                        str.Append("' border='0' align='absmiddle' />");
                        str.Append(mName);
                        str.Append("</a></td>");
                        str.Append("</tr>");
                    }
                    count++;
                }
                str.Append("</table>");
                str.Append("</td></tr></table>");
            }


            str.Append("<script>function chang(id){");
            count = 0;
            foreach (MenuModel menu in menus)
            {
                str.Append("document.getElementById('menu" + count + "').className='white';");
                count++;
            }
            str.Append("switch(id)	{");
            count = 0;
            foreach (MenuModel menu in menus)
            {
                str.Append("case " + count + ":document.getElementById('menu" + count + "').className='yellow';break;");
                count++;
            }
            str.Append("}}</script>");

            return str.ToString();
        }

        public static string GetFormatTitle(string title, int len)
        {
            if (title.Length > len)
                return title.Substring(0, len) + "...";
            return title;
        }

        /// <summary>
        /// 获取菜单个数
        /// </summary>
        /// <param name="type">菜单类型</param>
        /// <param name="pid">菜单父菜单ID</param>
        /// <returns>返回菜单个数</returns>
        public static int GetMenuCount(int type, int pid)
        {
            return MenuDAL.GetMenuCount(type, pid);
        }

        /// <summary>
        /// 获取登录时间
        /// </summary>
        /// <param name="mid"> 管理员编号 </param>
        /// <returns>返回时间</returns>
        public static DateTime GetLoginTime(string mid, int type)
        {
            if (type == 1)
            {
                return ManageDAL.GetLoginTime(mid);
            }
            else if (type == 2)
            {
                return StoreInfoDAL.GetLastLoginTime(mid);
            }
            else if (type == 3)
            {
                return MemberInfoDAL.GetLastLoginTime(mid);
            }
            else
            {
                return DateTime.Now;
            }
        }
        /// <summary>
        /// 根据服务编号获取服务机构名
        /// </summary>
        /// <param name="sid">服务机构编号</param>
        /// <returns>服务机构姓名</returns>
        public static string GetStoreName(string sid)
        {
            return ManageDAL.GetStoreNameByNumber(sid);
        }

        /// <summary>
        /// 根据管理编号获取管理员姓名
        /// </summary>
        /// <param name="mid">管理员编号</param>
        /// <returns>管理员姓名</returns>
        public static string GetManageName(string mid)
        {
            return ManageDAL.GetManageNameByNumber(mid);
        }

        /// <summary>
        /// 获取菜单头名称
        /// </summary>
        /// <param name="type">菜单类型</param>
        /// <param name="tid">菜单ID</param>
        /// <returns>菜单名称</returns>
        public static string GetTitleName(int type, int tid)
        {
            return MenuDAL.GetTitleName(type, tid);
        }

        /// <summary>
        /// 获取分公司姓名
        /// </summary>
        /// <param name="memeberid">分公司编号</param>
        /// <returns></returns>
        //public static string GetBranchName(string memeberid)
        //{
        //    return MemberInfoDAL.GetBranchName(memeberid);
        //}


        /// <summary>
        /// 获取会员姓名
        /// </summary>
        /// <param name="memeberid">会员编号</param>
        /// <returns></returns>
        public static string GetMemberName(string memeberid)
        {
            return MemberInfoDAL.GetMemberName(memeberid);
        }
    }
}
