using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

//Add Namespace
using System.Collections;
using System.Text;
using System.Data.SqlClient;
using BLL.CommonClass;
using DAL;
using System.Xml;
using Encryption;
/// <summary>
///RemarkInfo 的摘要说明
/// </summary>

namespace BLL.CommonClass
{
    public class RemarkInfo
    {
        public RemarkInfo()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// Remark记录格式串构建状态
        /// </summary>
        public enum RemarkState
        {
            /// <summary>
            /// 开工
            /// </summary>
            StartHeader,
            /// <summary>
            /// 项目
            /// </summary>
            StartItem,
            /// <summary>
            /// <summary>
            /// 收工
            /// </summary>
            StartFooter
        }

        private StringBuilder _remarkbuilder;
        private string _tablename; // 要记录表记录的名称
        private string _identity; // 要记录表记录的标识列(目前只设置单列)
        private int _item; // 记录数
        public RemarkState State;
        private Hashtable hash;
        private Hashtable hashDP;//解密时用

        private Hashtable hashNoVisible;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tablename">要记录表记录的名称</param>
        /// <param name="identity">要记录表记录的标识列(目前只设置单列)</param>
        public RemarkInfo(string tablename, string identity)
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            this._remarkbuilder = new StringBuilder();
            this._tablename = tablename.ToLower();
            this._identity = identity;
            this._item = 0;
            this.State = RemarkState.StartHeader;
            this.hashNoVisible = new Hashtable();

            _remarkbuilder.Append("<table width='100%' border='0' cellpadding='0' cellspacing='0' class='tablemb' align='center' style='white-space:nowrap'>");
        }

        /// <summary>
        /// 添加记录信息行(重载)
        /// </summary>
        /// <param name="id">标识列的值(条件列，可扩展)</param>
        public void AddRecord(int id)
        {
            AddRecord(id.ToString());
        }

        /// <summary>
        /// 添加记录信息行
        /// </summary>
        /// <param name="id">标识列的值(条件列，可扩展)</param>
        /// <param name="columns">需要的列数组</param>
        /// <param name="changetype">操作的类型（删除或修改）</param>
        public void AddRecord(string id, string[] columns, ChangeType changetype)
        {
            StringBuilder sqlbuilder = new StringBuilder();

            sqlbuilder.Append("select ");
            if (columns.Length > 0)
            {
                for (int i = 0; i <= columns.Length - 2; i++)
                {
                    sqlbuilder.Append(columns[i]);
                    sqlbuilder.Append(",");
                }
                // 最后不要加逗点
                sqlbuilder.Append(columns[columns.Length - 1]);
            }
            else
            {
                sqlbuilder.Append(" * ");
            }

            sqlbuilder.Append(" from ");
            sqlbuilder.Append(_tablename);
            sqlbuilder.Append(" where ");
            sqlbuilder.Append(_identity);
            sqlbuilder.Append(" = ");
            sqlbuilder.Append("'");
            sqlbuilder.Append(id);
            sqlbuilder.Append("'");

            SqlDataReader reader = DBHelper.ExecuteReader(sqlbuilder.ToString());

            string ss = sqlbuilder.ToString();
            ss = "";
            bool isVisible = false;
            string columnName = string.Empty;

            while (reader.Read())
            {
                // 添加列信息
                if (this.State == RemarkState.StartHeader)
                {
                    _remarkbuilder.Append("<tr align='center'>");

                    // 操作类型标题列
                    _remarkbuilder.Append("<th style='white-space:nowrap'>操作类型</th>");

                    for (int i = 0; i <= reader.FieldCount - 1; i++)
                    {
                        columnName = GetColumnName(reader.GetName(i));
                        
                        // 添加字段名称记录信息
                        _remarkbuilder.Append("<th style='white-space:nowrap'>");
                        _remarkbuilder.Append(columnName); // 字段名称
                        _remarkbuilder.Append("</th>");

                    }
                    _remarkbuilder.Append("</tr>");

                    this.State = RemarkState.StartItem; // 改变状态
                }

                if (this.State == RemarkState.StartItem)
                {
                    // 添加记录行信息
                    _remarkbuilder.Append("<tr align='center'>");

                    // 操作类型列
                    _remarkbuilder.Append("<td style='white-space:nowrap'>");
                    _remarkbuilder.Append(Enum.GetName(changetype.GetType(), changetype)); // 字段名称
                    _remarkbuilder.Append("</td>");

                    for (int i = 0; i <= reader.FieldCount - 1; i++)
                    {
                        if (hashNoVisible.Contains(i))
                            continue;

                        _remarkbuilder.Append("<td style='white-space:nowrap'>");
                        // 字段名称
                        if (hashDP.Contains(i))
                        {
                            _remarkbuilder.Append(reader[i] == null ? " &nbsp;" : decipher(reader[i].ToString().Trim(), hashDP[i].ToString().ToLower())); // 字段名称
                        }
                        else
                        {
                            _remarkbuilder.Append(reader[i] == null ? " &nbsp;" : reader[i].ToString().Trim()); // 字段名称
                        }
                        _remarkbuilder.Append("</td>");
                    }
                    _remarkbuilder.Append("</tr>");

                    this._item++; // 记录行加加
                }
            }

            reader.Close();
        }


        /// <summary>
        /// 添加记录信息行 有事务
        /// </summary>
        /// <param name="id">标识列的值(条件列，可扩展)</param>
        /// <param name="columns">需要的列数组</param>
        /// <param name="changetype">操作的类型（删除或修改）</param>
        public void AddRecordtran(SqlTransaction tran, string id, string[] columns, ChangeType changetype)
        {
            StringBuilder sqlbuilder = new StringBuilder();

            sqlbuilder.Append("select ");
            if (columns.Length > 0)
            {
                for (int i = 0; i <= columns.Length - 2; i++)
                {
                    sqlbuilder.Append(columns[i]);
                    sqlbuilder.Append(",");
                }
                // 最后不要加逗点
                sqlbuilder.Append(columns[columns.Length - 1]);
            }
            else
            {
                sqlbuilder.Append(" * ");
            }

            sqlbuilder.Append(" from ");
            sqlbuilder.Append(_tablename);
            sqlbuilder.Append(" where ");
            sqlbuilder.Append(_identity);
            sqlbuilder.Append(" = ");
            sqlbuilder.Append("'");
            sqlbuilder.Append(id);
            sqlbuilder.Append("'");

            SqlCommand cmd = new SqlCommand(sqlbuilder.ToString(), tran.Connection);
            cmd.Transaction = tran;
            SqlDataReader reader = cmd.ExecuteReader();
            //SqlDataReader reader = DBHelper.ExecuteReader(sqlbuilder.ToString());

            string ss = sqlbuilder.ToString();
            ss = "";

            bool isVisible = false;
            string columnName = string.Empty;

            while (reader.Read())
            {
                // 添加列信息
                if (this.State == RemarkState.StartHeader)
                {
                    _remarkbuilder.Append("<tr align='center'>");

                    // 操作类型标题列
                    _remarkbuilder.Append("<th style='white-space:nowrap'>操作类型</th>");

                    for (int i = 0; i <= reader.FieldCount - 1; i++)
                    {
                        columnName = GetColumnName(reader.GetName(i));
                        //columnName = DsSystem.TableMap.Columns.GetColumnName(reader.GetName(i), out  isVisible);
                        //	if (!isVisible)
                        //	{
                        //		hashNoVisible .Add (i , i);
                        //		continue ;
                        //	}
                        // 添加字段名称记录信息
                        _remarkbuilder.Append("<th style='white-space:nowrap'>");
                        _remarkbuilder.Append(columnName); // 字段名称
                        _remarkbuilder.Append("</th>");

                    }
                    _remarkbuilder.Append("</tr>");

                    this.State = RemarkState.StartItem; // 改变状态
                }

                if (this.State == RemarkState.StartItem)
                {
                    // 添加记录行信息
                    _remarkbuilder.Append("<tr align='center'>");

                    // 操作类型列
                    _remarkbuilder.Append("<td style='white-space:nowrap'>");
                    _remarkbuilder.Append(Enum.GetName(changetype.GetType(), changetype)); // 字段名称
                    _remarkbuilder.Append("</td>");

                    for (int i = 0; i <= reader.FieldCount - 1; i++)
                    {
                        if (hashNoVisible.Contains(i))
                            continue;

                        _remarkbuilder.Append("<td style='white-space:nowrap'>");
                        if (hashDP.Contains(i))
                        {
                            _remarkbuilder.Append(reader[i] == null ? " &nbsp;" : decipher(reader[i].ToString().Trim(),hashDP[i].ToString().ToLower())); // 字段名称
                        }
                        else
                        { 
                         _remarkbuilder.Append(reader[i] == null ? " &nbsp;" : reader[i].ToString().Trim()); // 字段名称
                        }
                       _remarkbuilder.Append("</td>");
                    }
                    _remarkbuilder.Append("</tr>");

                    this._item++; // 记录行加加
                }
            }

            reader.Close();
        }
        /// <summary>
        /// 添加记录信息行
        /// </summary>
        /// <param name="id">标识列的值(条件列，可扩展)</param>
        /// <param name="columns">需要的列数组</param>
        public void AddRecord(string id, string[] columns)
        {
            if (this._item > 0)
                this.AddRecord(id, columns, ChangeType.Modify);
            else
                this.AddRecord(id, columns, ChangeType.Delete);
        }
        /// <summary>
        /// 添加记录信息行 有事务
        /// </summary>
        /// <param name="id">标识列的值(条件列，可扩展)</param>
        /// <param name="columns">需要的列数组</param>
        public void AddRecordtran(SqlTransaction tran, string id, string[] columns)
        {
            if (this._item > 0)
                this.AddRecordtran(tran, id, columns, ChangeType.Modify);
            else
                this.AddRecordtran(tran, id, columns, ChangeType.Delete);
        }

        /// <summary>
        /// 添加记录信息行
        /// </summary>
        /// <param name="id">标识列的值(条件列，可扩展)</param>
        public void AddRecord(string id)
        {
            string[] columns = new string[0];
            this.AddRecord(id, columns);
        }
        /// <summary>
        /// 添加记录信息行
        /// </summary>
        /// <param name="id">标识列的值(条件列，可扩展)</param>
        public void AddRecordtran(SqlTransaction tran, string id)
        {
            string[] columns = new string[0];
            this.AddRecordtran(tran, id, columns);
        }

        /// <summary>
        /// 构建完毕
        /// </summary>
        public void EndRemark()
        {
            this.State = RemarkState.StartFooter;
            _remarkbuilder.Append("</table>");
            hash.Clear();
            hashDP.Clear();
        }

        #region Text
        /// <summary>
        /// remark string
        /// </summary>
        public string Text
        {
            get
            {
                // 自动结束
                if (this.State == RemarkState.StartItem)
                    EndRemark();

                HttpResponse response = HttpContext.Current.Response;
                // 输出调试信息
                switch (this.State)
                {
                    case RemarkState.StartHeader:
                        response.Write("未调用：AddRecord()或EndRemark()");
                        response.End();
                        break;
                    // return "未调用：AddRecord()或EndRemark()";
                    case RemarkState.StartItem:
                        response.Write("未调用：AddRecord()或EndRemark()");
                        response.End();
                        break;
                    // return "未调用：AddRecord()或EndRemark()";
                    case RemarkState.StartFooter:
                        return _remarkbuilder.ToString();
                    default:
                        return string.Empty;
                }
                return string.Empty;
            }
        }
        #endregion

        #region 表记录行转换为Remark记录格式串(Delete)
        /// <summary>
        /// 表记录行转换为Remark记录格式串(Delete)
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="identity"></param>
        /// <param name="idvalue"></param>
        /// <returns></returns>
        public string ______RemarkStringBuilder(string tablename, string identity, string idvalue)
        {
            StringBuilder sqlbuilder = new StringBuilder("select * from @tablename where @identity = @idvalue");
            StringBuilder remarkbuilder = new StringBuilder(); // 返回记录信息字串构建者

            sqlbuilder.Replace("@tablename", tablename);
            sqlbuilder.Replace("@identity", identity);
            sqlbuilder.Replace("@idvalue", idvalue);

            SqlDataReader reader = DBHelper.ExecuteReader(sqlbuilder.ToString());

            while (reader.Read())
            {
                remarkbuilder.Append("<table><tr>");
                for (int i = 0; i <= reader.FieldCount - 1; i++)
                {
                    // 添加字段名称记录信息
                    remarkbuilder.Append("<td>");
                    remarkbuilder.Append(reader.GetName(i)); // 字段名称
                    remarkbuilder.Append("</td>");
                }
                // 记录行
                remarkbuilder.Append("</tr>");

                remarkbuilder.Append("<tr>");
                for (int i = 0; i <= reader.FieldCount - 1; i++)
                {
                    remarkbuilder.Append("<td>");
                    remarkbuilder.Append(reader[i].ToString()); // 字段名称
                    remarkbuilder.Append("</td>");
                }
                remarkbuilder.Append("</tr></table>");
            }
            reader.Close();
            return string.Empty;
        }
        #endregion

        public string GetColumnName(string cname)
        {
            if (hash == null || hashDP==null)
            {
                hash = new Hashtable();
                hashDP = new Hashtable();
                GetHash();
            }
            cname = cname.ToLower();
            if (hash.Contains(cname))
            {
                return hash[cname].ToString();
            }
                return " ";
        }
        /// <summary>
        /// 获取表头
        /// </summary>
        /// <param name="Server"></param>
        public void GetHash()
        {
            XmlDocument doc = new XmlDocument();
            
            try
            {
                doc.Load(System.Web.HttpContext.Current.Server.MapPath("../app_code/CityFiles.xml"));
            }
            catch
            {
                try
                {
                    doc.Load(System.Web.HttpContext.Current.Server.MapPath("app_code/CityFiles.xml"));
                }
                catch
                { 
                    return;
                }
               
            }
            XmlNodeList nodes = doc.SelectNodes("/room/table");
            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].Attributes["name"].Value.ToLower() == _tablename)
                {
                    XmlNodeList list = nodes[i].ChildNodes;
                    for (int j = 0; j < list.Count; j++)
                    {
                        hash.Add(list[j].Attributes["name"].Value.ToLower(), list[j].ChildNodes[0].InnerText);
                        //hash.Add(list[j].Attributes["name"].Value.ToLower(), "<%key('" + list[j].Attributes["tran"].Value + "', '" + list[j].ChildNodes[0].InnerText + "') %>");
                        if (list[j].Attributes["Decipher"] != null)
                        {
                            hashDP.Add(j, list[j].Attributes["Decipher"].Value.ToLower());
                        }
                    } 
                }
            }
        }


        /// <summary>
        /// 解密字段
        /// </summary>
        /// <param name="val">值</param>
        /// <param name="decipher">方法</param>
        /// <returns></returns>
        public string decipher(string val, string decipher)
        {
            string va = string.Empty;
            switch (decipher)
            {
                case "name": va = Encryption.Encryption.GetDecipherName(val);
                    break;
                case "tele": va = Encryption.Encryption.GetDecipherTele(val);
                    break;
                case "address": va = Encryption.Encryption.GetDecipherAddress(val);
                    break;
                case "card": va = Encryption.Encryption.GetDecipherCard(val);
                    break;
                case "number": va = Encryption.Encryption.GetDecipherNumber(val);
                    break;
                default: va = val;
                    break;
            }
            return va;
        }
    }
}
