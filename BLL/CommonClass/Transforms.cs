using System;
using System.Web;
using System.Web.UI;
using System.Text.RegularExpressions;

namespace BLL.CommonClass
{
	/// <summary>
    /// 字符串的变换，包括格式转换，加密和解密
	/// </summary>
	public class Transforms
	{
		public Transforms()	{}

		/// <summary>
		/// 返回能够弹出提示框的 javascript 代码
		/// </summary>
		/// <param name="content">要提示的内容</param>
		/// <returns>javascript 代码</returns>
		public static string ReturnAlert(string content)
		{
			string retVal;
			retVal = "<script language='javascript'>alert('" + content.Replace("'"," ").Replace("\r"," ").Replace("\n","").Replace("\t"," ") + "');</script>";
			return retVal;
		}

		/// <summary>
		/// 返回能够弹出提示框,然后重定向的 javascript 代码
		/// </summary>
		/// <param name="content">要提示的内容</param>
		/// <param name="redirectUrl">要重定向的页面</param>
		/// <returns>javascript 代码</returns>
		public static string ReturnAlertAndRedirect(string content,string redirectUrl)
		{
			string retVal;

			retVal = "<script language='javascript'>alert('" + content.Replace("'"," ").Replace("\r"," ").Replace("\n","").Replace("\t"," ") + "');" + "location.href('" + redirectUrl + "');</script>";

			return retVal ;
		}

        /// <summary>
        /// 些处输入要提示的内容
        /// </summary>
        /// <param name="command"></param>
        public static void JSAlert(string commandText)
        {
            Regex rxb = new Regex(@"(\r\n)|(\r)|(\n)", RegexOptions.IgnoreCase);
            commandText = rxb.Replace(commandText, "");
            Page page = HttpContext.Current.Handler as Page;
            if (page != null)
            {
                Random r = new Random(100);
                int i = r.Next();
                //page.RegisterClientScriptBlock(DateTime.Now.Millisecond .ToString ()+i.ToString(),"<script language='javascript'>alert(\""+commandText+"\");</script>");
                page.RegisterStartupScript(DateTime.Now.Millisecond.ToString() + i.ToString(), "<script language='javascript'>alert(\"" + commandText + "\");</script>");
            }
        }

        /// <summary>
        /// 执行JS脚本模块代码
        /// </summary>
        /// <param name="command"></param>
        public static void JSExec(string command)
        {
            Page page = HttpContext.Current.Handler as Page;
            if (page != null)
            {
                Random r = new Random(100);
                int i = r.Next();
                page.RegisterStartupScript(DateTime.Now.Millisecond.ToString() + i.ToString(), "<script language='javascript'>" + command + "</script>");
            }
        }


		
		/// <summary>
		/// 登陆时的用户名加密
		/// </summary>
		/// <param name="bianhao">要加密的编号</param>
		/// <returns>得到的加密结果</returns>
		public static string LoginTransform(string bianhao)
		{
			string retVal = "";					//存放加密的结果
			int length = bianhao.Length;		//存放字符串的长度
			int chrInt;							//存放字符的 ASC 码


			for ( int i=0;i<length;i++ )
			{
				chrInt = Convert.ToInt32(bianhao[length -i -1]);
				chrInt += Convert.ToInt32(length/2);
				chrInt -= ((i+1) % Convert.ToInt32(length/2));
				retVal = retVal + Convert.ToChar(chrInt).ToString();
			}

			return retVal;
		}

		/// <summary>
		/// 登陆时的用户名解密
		/// </summary>
		/// <param name="bianhao">要解密的编号</param>
		/// <returns>得到的解密结果</returns>
		public static string LoginReTransform(string bianhao)
		{
			string retVal = "";					//存放解密的结果
			int length = bianhao.Length;		//存放字符串的长度
			int chrInt;							//存放字符的 ASC 码

			bianhao = bianhao.Replace("＜","<");
			bianhao = bianhao.Replace("＞",">");

			for ( int i=0;i<length;i++ )
			{
				chrInt = Convert.ToInt32(bianhao[i]);
				chrInt -= Convert.ToInt32(length/2);
				chrInt += ((i+1) % Convert.ToInt32(length/2));
				retVal =  Convert.ToChar(chrInt).ToString() + retVal;
			}

			return retVal;
		}





	}
}
