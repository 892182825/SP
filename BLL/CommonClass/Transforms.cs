using System;
using System.Web;
using System.Web.UI;
using System.Text.RegularExpressions;

namespace BLL.CommonClass
{
	/// <summary>
    /// �ַ����ı任��������ʽת�������ܺͽ���
	/// </summary>
	public class Transforms
	{
		public Transforms()	{}

		/// <summary>
		/// �����ܹ�������ʾ��� javascript ����
		/// </summary>
		/// <param name="content">Ҫ��ʾ������</param>
		/// <returns>javascript ����</returns>
		public static string ReturnAlert(string content)
		{
			string retVal;
			retVal = "<script language='javascript'>alert('" + content.Replace("'"," ").Replace("\r"," ").Replace("\n","").Replace("\t"," ") + "');</script>";
			return retVal;
		}

		/// <summary>
		/// �����ܹ�������ʾ��,Ȼ���ض���� javascript ����
		/// </summary>
		/// <param name="content">Ҫ��ʾ������</param>
		/// <param name="redirectUrl">Ҫ�ض����ҳ��</param>
		/// <returns>javascript ����</returns>
		public static string ReturnAlertAndRedirect(string content,string redirectUrl)
		{
			string retVal;

			retVal = "<script language='javascript'>alert('" + content.Replace("'"," ").Replace("\r"," ").Replace("\n","").Replace("\t"," ") + "');" + "location.href('" + redirectUrl + "');</script>";

			return retVal ;
		}

        /// <summary>
        /// Щ������Ҫ��ʾ������
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
        /// ִ��JS�ű�ģ�����
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
		/// ��½ʱ���û�������
		/// </summary>
		/// <param name="bianhao">Ҫ���ܵı��</param>
		/// <returns>�õ��ļ��ܽ��</returns>
		public static string LoginTransform(string bianhao)
		{
			string retVal = "";					//��ż��ܵĽ��
			int length = bianhao.Length;		//����ַ����ĳ���
			int chrInt;							//����ַ��� ASC ��


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
		/// ��½ʱ���û�������
		/// </summary>
		/// <param name="bianhao">Ҫ���ܵı��</param>
		/// <returns>�õ��Ľ��ܽ��</returns>
		public static string LoginReTransform(string bianhao)
		{
			string retVal = "";					//��Ž��ܵĽ��
			int length = bianhao.Length;		//����ַ����ĳ���
			int chrInt;							//����ַ��� ASC ��

			bianhao = bianhao.Replace("��","<");
			bianhao = bianhao.Replace("��",">");

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
