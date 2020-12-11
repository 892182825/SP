using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Xml;
using System.Text;
using Model;

namespace BLL.FileOperateBLL
{
    public class ForXML
    {
        public ForXML()
        {

        }

        #region 公共属性

        
        /// <summary>
        /// 配置文件夹路径
        /// </summary>
        private string directoryPath = ConfigDirectoryPath;
        /// <summary>
        /// 配置文件目录路径
        /// </summary>
        public static string ConfigDirectoryPath
        {
            get
            {
                string phyPath = AppDomain.CurrentDomain.BaseDirectory;
                string directoryName = "App_Code";
                return Path.Combine(phyPath, directoryName);
            }
        }

        /// <summary>
        /// 短信配置文件路径
        /// </summary>
        private string configSMSPath = ConfigSMSPath;
        /// <summary>
        /// 配置文件路径
        /// </summary>
        public static string ConfigSMSPath
        {
            get
            {
                string directoryPath =ConfigDirectoryPath;             
                string fileName = "configSMS.xml";
                return Path.Combine(directoryPath, fileName);
            }
        }

        
		/// <summary>
		/// 语言文件路径
		/// </summary>
		public static string LanguageXMLDirPath
		{
			get
			{
				string phyPath = AppDomain.CurrentDomain.BaseDirectory;				
				string fileName = "Language/language.xml";
				return Path.Combine (phyPath,fileName);
			}
		}//Initialization language files
        #endregion

        #region 配置文件操作

        /// <summary>
        /// 创建用于存放配置文件的文件夹
        /// </summary>
        /// <param name="path">文件夹路径，项目根目录下的Config文件夹</param>
        /// <returns></returns>
        public bool CreateDirectory(string path)
        {
            bool flag = false;
            try
            {
                if (!Directory.Exists(path))
                {
                    DirectoryInfo di = Directory.CreateDirectory(path);
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return flag;
            

        }
       
        /// <summary>
        ///  保存手机短信配置信息 
        /// </summary>
        /// <param name="smsUrl">网关URL</param>
        /// <param name="userName">帐户</param>
        /// <param name="pwd">帐户密码</param>
        /// <param name="isOpen">是否开启短信发送功能，默认开启，1：开启，0：关闭</param>
        /// <returns></returns>
        public bool SaveConfigSMS(string smsUrl, string userName, string pwd,int isOpen)	//,int iCurUrl,string curUrl
        {
            bool flag = false;
            try
            {
                string savePath = this.configSMSPath;
                XmlDocument doc = new XmlDocument();
                bool exists = System.IO.File.Exists(savePath);
                if (!exists)
                {//生成		
                    if (!Directory.Exists(directoryPath))
                    {
                        CreateDirectory(directoryPath);
                    }
                    if (smsUrl == "")
                    {
                        smsUrl = "http://www.139000.com/send/gsend.asp?";
                    }
                    if (userName == "")
                    {
                        userName = "";  //zx001  
                    }
                    if (pwd == "")
                    {
                        pwd = "";       //zx001qc
                    }
                    if (isOpen !=0&&isOpen !=1)
                    {
                        isOpen = 1;
                    }
                    
                    doc.LoadXml("<Configs></Configs>");               //创建根结点		
                    XmlNode root = doc.SelectSingleNode("Configs");
                    XmlElement xl = doc.CreateElement("SmsUrl");
                    xl.SetAttribute("id", "smsUrl");
                    xl.SetAttribute("value", smsUrl);
                    xl.SetAttribute("text", smsUrl);
                    root.AppendChild(xl);

                    xl = doc.CreateElement("UserName");
                    xl.SetAttribute("id", "userName");
                    xl.SetAttribute("value", userName);
                    xl.SetAttribute("text", userName);
                    root.AppendChild(xl);

                    xl = doc.CreateElement("Pwd");
                    xl.SetAttribute("id", "pwd");
                    xl.SetAttribute("value", pwd);		
                    xl.SetAttribute("text", pwd);
                    root.AppendChild(xl);


                    xl = doc.CreateElement("IsOpen");
                    xl.SetAttribute("id", "isOpen");
                    xl.SetAttribute("value", isOpen.ToString ());             //0-己停止使用报单,1-开启短信功能
                    xl.SetAttribute("text", isOpen.ToString());  
                    root.AppendChild(xl);
                    doc.Save(savePath);
                    flag = true;
                }
                else
                {//保存

                    doc.Load(savePath);
                    XmlElement xe = null;
                    if (smsUrl !="")
                    {
                        xe = (XmlElement)doc.SelectSingleNode("Configs/SmsUrl");
                        if (xe != null)
                        {
                            xe.SetAttribute("id", "smsUrl");
                            xe.SetAttribute("value", smsUrl);
                            xe.SetAttribute("text", smsUrl);
                            doc.Save(savePath); //保存 
                        }
                    }
                    if (userName !="")
                    {
                        xe = (XmlElement)doc.SelectSingleNode("Configs/UserName");
                        if (xe != null)
                        {
                            xe.SetAttribute("id", "userName");
                            xe.SetAttribute("value", userName);
                            xe.SetAttribute("text", userName);
                            doc.Save(savePath); //保存 
                        }
                    }
                    if (pwd != "")
                    {
                        xe = (XmlElement)doc.SelectSingleNode("Configs/Pwd");
                        if (xe != null)
                        {
                            xe.SetAttribute("id", "pwd");
                            xe.SetAttribute("value", pwd);
                            xe.SetAttribute("text", pwd);
                            doc.Save(savePath); //保存 
                        }
                    }
                    if (isOpen==1||isOpen ==0)
                    {
                        xe = (XmlElement)doc.SelectSingleNode("Configs/IsOpen");
                        if (xe != null)
                        {
                            xe.SetAttribute("id", "isOpen");
                            xe.SetAttribute("value", isOpen.ToString());
                            xe.SetAttribute("text", isOpen.ToString());
                            doc.Save(savePath); //保存 
                        }
                    }               
                    flag = true;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return flag;
        }
        /// <summary>
        ///保存手机短信配置信息（数据库）
        /// </summary>
        /// <param name="smsUrl">网关URL</param>
        /// <param name="userName">帐户</param>
        /// <param name="pwd">帐户密码</param>
        /// <param name="isOpen">是否开启短信发送功能，默认开启，1：开启，0：关闭</param>
        /// <returns></returns>
        public bool SaveSMSConfig(string smsUrl, string username, string pwd, int isOpen) {
            string ExSql = "update SMSConfig set SmsUrl=@Smsurl,username=@username,pwd=@pwd,isopen=@isopen";
            SqlParameter[] sp = { new SqlParameter("@Smsurl", smsUrl), new SqlParameter("@username", username), new SqlParameter("@pwd", pwd), new SqlParameter("@isopen", isOpen) };
            int var = DAL.DBHelper.ExecuteNonQuery(ExSql, sp, CommandType.Text);
            if (var == 0)
            {
                ExSql = "insert into SMSConfig(SmsUrl,username,pwd,isopen) values('http://www.139000.com/send/gsend.asp?','常爱华','sh2009',1)";
                var = DAL.DBHelper.ExecuteNonQuery(ExSql);
            }
            if (var > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 读指定节点的指定属性值
        /// </summary>
        /// <param name="cp"></param>
        /// <param name="regx">XPath路径Configs/([SmsUrl|UserName|Pwd|IsOpen])</param>
        /// <returns></returns>
        public static object ReadConfigSMS(Model.ConfigProperty cp, string regx)
        {
            object result = string.Empty;
            try
            {
                string savePath =ConfigSMSPath;
                XmlDocument doc = new XmlDocument();
                bool exists = System.IO.File.Exists(savePath);
                if (exists)
                {//存在								
                    doc.Load(savePath);
                    XmlElement xe = (XmlElement)doc.SelectSingleNode(regx);	//"Configs/MaxQishu"

                    switch (cp)
                    {
                        case ConfigProperty.config_id:
                            result = xe.GetAttribute("id");
                            break;
                        case ConfigProperty.config_sign:
                            result = xe.GetAttribute("sign");
                            break;
                        case ConfigProperty.config_text:
                            result = xe.GetAttribute("text");
                            break;
                        case ConfigProperty.config_value:
                            result = xe.GetAttribute("value");
                            break;
                        default:
                            result = xe.GetAttribute("value");
                            break;
                    }
                }

            }
            catch (Exception ex)
            {
            }
            return result;
        }
        /// <summary>
        /// 读取指定字段的值
        /// </summary>
        /// <param name="regx">字段名称</param>
        /// <returns></returns>
        public static object ReadSMSConfig(string regx) {
            string res = "";
            try
            {
                string sql = "select top 1 * from SMSConfig";
                DataTable dt = DAL.DBHelper.ExecuteDataTable(sql);
                if (dt.Rows.Count > 0)
                {
                    if (regx != "IsOpen") {
                        res = dt.Rows[0][regx].ToString();
                    }
                    else if (regx == "IsOpen") {
                        try
                        {
                            if (int.Parse(dt.Rows[0]["IsOpen"].ToString()) != 0 && int.Parse(dt.Rows[0]["IsOpen"].ToString()) != 1)
                            {
                                res = "1";
                            }
                            else {
                                res = dt.Rows[0]["IsOpen"].ToString();
                            }
                        }
                        catch {
                            res = "1";   //默认短信是开放
                        }
                    }
                   
                }
            }
            catch {
                res = "";
            }
            return res;
        }

        #endregion 

        #region 多语言操作

        /// <summary>
		/// 初始化语言XML，每次数据库中的翻译内容有添加、修改、删除操作时，都应重新执行此方法重新初始化语言文件，以使数据库与XML文件的一致
		/// </summary>
		public bool InitializationLanguage()
		{
			string ExSql=string.Empty ;
			bool flag=false;
			try
			{					
				string filePath=LanguageXMLDirPath;
				bool exists = System.IO.File.Exists(filePath);
				if(exists)
				{
					File.Delete (filePath);
				}
				ExSql="select languageCode from language order by id";
				XmlDocument doc = new XmlDocument();
				doc.LoadXml("<Languages></Languages>");               //创建根结点		MaxQishu、QiStop,QiCreate，QiStopFlag
				XmlNode root = doc.SelectSingleNode("Languages");
				DataTable dt=DAL.DBHelper.ExecuteDataTable (ExSql);
				string nodeName=string.Empty ;
				string keyCode=string.Empty ;
				string languageCode=string.Empty ;
				string translateValue=string.Empty ;
				XmlElement xl;
				ExSql=" select * from t_translation  order by keyCode asc" ;
				SqlDataReader sdr=DAL.DBHelper.ExecuteReader (ExSql);
				while(sdr.Read ())
				{
					keyCode=sdr["keyCode"].ToString ().Trim ().ToUpper ();
					xl = doc.CreateElement("L"+keyCode);					
					foreach(DataRow dr in dt.Rows )
					{			
						languageCode=dr["languageCode"].ToString ().Trim ();		//语言代码
						translateValue=sdr[languageCode].ToString ().Trim ();		//语言代码对应的翻译内容

						XmlElement childXl=doc.CreateElement (languageCode);
						childXl.InnerText =translateValue;	
						xl.AppendChild (childXl);
					}
					root.AppendChild(xl);
				}
				sdr.Close ();				
				doc.Save(filePath);
				flag=true;
			}
			catch(Exception ex)
			{
				throw ex;
			}
			return flag;
		}
	
        /// <summary>
        /// 根据键值读取对应翻译内容
        /// </summary>
        /// <param name="keyCode">翻译键值</param>
        /// <returns></returns>
		public static object ReadLanguageByKeyCode(string keyCode)
		{
			object result = string.Empty;
			try
			{
				if(System.Web .HttpContext .Current .Session ["LanguageCode"]!=null)
				{
					string languageCode=System.Web .HttpContext .Current .Session ["LanguageCode"].ToString ().ToUpper ();
					string regx="Languages/L"+keyCode+"/"+languageCode;	
					result=ReadLanguage(regx);
					if(result.ToString ()=="")
						result=null;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}

        /// <summary>
        /// 读取指字节点内容
        /// </summary>
        /// <param name="regx">xPath</param>
        /// <returns></returns>
		public static object ReadLanguage(string regx)
		{
			object result = string.Empty;
			try
			{
				XmlDocument doc = new XmlDocument();
				string filePath=LanguageXMLDirPath;
				bool exists = System.IO.File.Exists(filePath);
                if (exists)
                {//存在								
                    doc.Load(filePath);
                    XmlElement xe = (XmlElement)doc.SelectSingleNode(regx);
                    try
                    {
                        if (xe != null && !xe.Equals(DBNull.Value))
                            result = xe.InnerText;
                    }
                    catch
                    {
                        result= "";
                    }
                }
                else
                {
                    throw new Exception("请选初始化多语言文件！");
                }

			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}

        #endregion 

    }
}
