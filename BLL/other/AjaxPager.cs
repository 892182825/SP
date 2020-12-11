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
using System.ComponentModel;
using Model.Other;
using DAL;
using System.Text;
using System.IO;
using System.Data.SqlClient;

namespace BLL.other
{
    /// <summary>
    ///AjaxPager 的摘要说明
    /// </summary>
    [ToolboxData("<{0}:AjaxPager runat=server></{0}:AjaxPager>"), DefaultProperty("TotalRecord")]
    public class AjaxPager : WebControl, ICallbackEventHandler
    {
        public AjaxPager()
            : base(HtmlTextWriterTag.Div)
        {
            this._BarBackGroundColor = "#FFFFFF";
            this._BarLinkColor = "Navy";
            this._BarCurrentColor = "#EEEEEE";
            this._TotalRecord = 50;
            this._TotalPage = 0;
            this._CurrentIndex = 1;
            this._ItemSize = 10;
            this._Info = new PageInfo();
            base.Load += new EventHandler(this.AjaxPager_Load);
        }


        private string _BarBackGroundColor;
        private string _BarCurrentColor;
        private string _BarLinkColor;
        private int _CurrentIndex;
        private PageInfo _Info;
        private int _ItemSize;
        private int _TotalPage;
        private int _TotalRecord;

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            writer.AddStyleAttribute("White-space", "nowrap");
            writer.AddStyleAttribute("padding-top", "2px");
            writer.AddStyleAttribute("padding-bottom", "2px");
            writer.AddStyleAttribute("color", "#949494");
            writer.AddStyleAttribute("font-weight", "bold");
            writer.AddStyleAttribute("background-color", this.BarBackGroundColor);
            base.AddAttributesToRender(writer);
        }

        private void AjaxPager_Load(object sender, EventArgs e)
        {
            string script = "function AjaxPagerCallBack(returnData){var parts =returnData.split('[_]'); document.getElementById('" + this.UniqueID.Replace('$', '_') + "').innerHTML = parts[0];document.getElementById('" + this.Info.ContainID + "').innerHTML=parts[1]}";
            this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "AjaxPagerCallBack", script, true);
        }

        private GridView getRpt()
        {
            return (Page.FindControl(this.Info.RepeaterUniqueID) as GridView);
        }

        public void BindData(SqlParameter[] paras)
        {
            GridView rpt = this.getRpt();
            rpt.Visible = true;
            int count = 0;
            DataTable datasource = GetPageData(this.Info.TableName, this.Info.Fields, this.Info.IdentityField, this.Info.PageSize, this.PageIndex, this.Info.IsDesc, this.Info.Content, paras, out count);

            this.TotalRecord = count;

            rpt.DataSource = datasource;
            rpt.DataBind();
        }

        public void BindData()
        {
            GridView rpt = this.getRpt();
            rpt.Visible = true;
            int count = 0;
            DataTable datasource = GetPageData(this.Info.TableName, this.Info.Fields, this.Info.IdentityField, this.Info.PageSize, this.PageIndex, this.Info.IsDesc, this.Info.Content, new SqlParameter[0], out count);

            this.TotalRecord = count;

            rpt.DataSource = datasource;
            rpt.DataBind();
        }

        public DataTable GetPageData(string tblName, string strGetFields, string fldName, int pageSize, int pageIndex, bool isDesc, string strWhere,SqlParameter[] paraWhere, out int count)
        {
            string sql = string.Format("select count(1) from [{0}]", tblName);
            if (!string.IsNullOrEmpty(strWhere))
            {
                sql = sql + string.Format(" where {0}", strWhere);
            }

            try
            {
                if (paraWhere.Length > 0)
                    count = Convert.ToInt32(DBHelper.ExecuteScalar(sql,paraWhere,CommandType.Text));
                else
                    count = Convert.ToInt32(DBHelper.ExecuteScalar(sql));
            }
            catch (SqlException)
            {
                count = -1;
            }
            
            //-----获取数据 
            string strSQL = "";
            string strTmp = "";
            string strOrder = "";

            if (isDesc)
            {
                strTmp = "<(select min";
                strOrder = " order by " + fldName + " desc";
            }
            else
            {
                strTmp = "<(select max";
                strOrder = " order by " + fldName + " asc";
            }

            if (pageIndex == 1)
            {
                if (!string.IsNullOrEmpty(strWhere))
                    strSQL = " select top " + pageSize + " " + strGetFields + "  from " + tblName + " where " + strWhere + " " + strOrder;
                else
                    strSQL = " select top " + pageSize + " " + strGetFields + "  from " + tblName + " " + strOrder;
            }
            else
            {
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strSQL = "select top " + pageSize + " " + strGetFields + "  from "
                                    + tblName + " where " + fldName + "" + strTmp + "("
                                    + fldName + ") from (select top " + ((pageIndex - 1) * pageSize) + " "
                                    + fldName + " from " + tblName + " where " + strWhere + " "
                                    + strOrder + ") as tblTmp) and " + strWhere + " " + strOrder;
                }
                else
                {
                    strSQL = "select top " + pageSize + " " + strGetFields + "  from " 
                                    + tblName + " where " + fldName + "" + strTmp + "(" + fldName + ") from (select top " 
                                    + ((pageIndex - 1) * pageSize) + " " + fldName + " from " + tblName + " " 
                                    + strOrder + ") as tblTmp)" + strOrder;
                }
            }

            if (paraWhere.Length > 0)
                return DBHelper.ExecuteDataTable(strSQL,paraWhere,CommandType.Text);
            else
                return DBHelper.ExecuteDataTable(strSQL, CommandType.Text);
            //SqlParameter[] paras = new SqlParameter[8];
            //paras[0] = new SqlParameter("tblName", tblName);
            //paras[1] = new SqlParameter("strGetFields", strGetFields);
            //paras[2] = new SqlParameter("fldName", fldName);
            //paras[3] = new SqlParameter("pageSize", pageSize);
            //paras[4] = new SqlParameter("pageIndex", pageIndex);
            //paras[5] = new SqlParameter("doCount", false);
            //paras[6] = new SqlParameter("orderType", isDesc);
            //paras[7] = new SqlParameter("strWhere", strWhere);
            //return DBHelper.ExecuteDataTable("sp_pager", paras, CommandType.StoredProcedure);

        }


        private string GetContents()
        {
            int num7;
            int num8;
            this._TotalPage = (((this.TotalRecord / this.Info.PageSize) * this.Info.PageSize) == this.TotalRecord) ? (this.TotalRecord / this.Info.PageSize) : ((this.TotalRecord / this.Info.PageSize) + 1);
            int num = ((this.PageIndex - 1) * this.Info.PageSize) + 1;
            int num2 = Math.Min(this.PageIndex * this.Info.PageSize, this.TotalRecord);
            string str = string.Format("[每页<span style='color:#CC0000'>{0}({1}-{2})</span>条&nbsp;&nbsp;共<span style='color:#CC0000'>{3}</span>条<span style='color:#CC0000'>{4}</span>页]", new object[] { this.Info.PageSize, num, num2, this.TotalRecord, this.TotalPage });
            StringBuilder builder = new StringBuilder();
            string str2 = "#000000";
            int num3 = this.TotalPage - ((this.TotalPage / this.BarSize) * this.BarSize);
            int num4 = (this.PageIndex - 1) / this.BarSize;
            int num5 = 1 + (this.BarSize * num4);
            int num6 = (((num4 + 1) * this.BarSize) > this.TotalPage) ? this.TotalPage : ((num4 + 1) * this.BarSize);
            if ((this.TotalRecord == 0) || (this.TotalPage == 0))
            {
                builder.AppendFormat("<span style='color:'{0};margin:auto 3px;'>0</span>", str2);
                builder.AppendFormat(" [共<span style='color:#CC0000'>0</span>页/当前第<span style='color:#CC0000'>0</span>页   共<span style='color:#CC0000'>0</span>条记录,当前记录数<span style='color:#CC0000'>0</span>到<span style='color:#CC0000'>0</span>]", new object[0]);
                return builder.ToString();
            }
            if (this.TotalPage <= this.BarSize)
            {
                for (num7 = 1; num7 <= this.TotalPage; num7++)
                {
                    str2 = (this.PageIndex == num7) ? "#CC0000" : "#000000";
                    if (this.PageIndex == num7)
                    {
                        builder.AppendFormat("<a id='{0}' style='color:{1};margin:auto 3px;'>{2}</a>", this.UniqueID, str2, num7);
                    }
                    else
                    {
                        builder.AppendFormat("<a id='{0}' style='color:{1};margin:auto 3px;' href=\"javascript:{2}\">{3}</a>", new object[] { this.UniqueID, str2, this.Page.ClientScript.GetCallbackEventReference(this, num7.ToString(), "AjaxPagerCallBack", null), num7 });
                    }
                }
                builder.AppendFormat(" {0}", str);
                return builder.ToString();
            }
            for (num7 = num5; num7 <= num6; num7++)
            {
                str2 = (this.PageIndex == num7) ? "#CC0000" : "#000000";
                if (this.PageIndex == num7)
                {
                    builder.AppendFormat("<a id={0}' style='color:{1};margin:auto 3px;'>{2}</a>", this.UniqueID, str2, num7);
                }
                else
                {
                    builder.AppendFormat("<a id='{0}' style='color:{1};margin:auto 3px;' href=\"javascript:{2}\">{3}</a>", new object[] { this.UniqueID, str2, this.Page.ClientScript.GetCallbackEventReference(this, num7.ToString(), "AjaxPagerCallBack", null), num7 });
                }
            }
            if ((this.PageIndex <= this.BarSize) && (this.TotalPage > this.BarSize))
            {
                builder.AppendFormat("<a id='{0}' href=\"javascript:{1}\">下一页</a>", this.UniqueID, this.Page.ClientScript.GetCallbackEventReference(this, Convert.ToString((int)(this.BarSize + 1)), "AjaxPagerCallBack", null));
            }
            if ((this.PageIndex > this.BarSize) && ((this.TotalPage - this.PageIndex) >= num3))
            {
                num8 = num4 * this.BarSize;
                int num9 = ((num4 + 1) * this.BarSize) + 1;
                builder.Insert(0, string.Format("<a id='{0}' href=\"javascript:{1}\">上一页</a>", this.UniqueID, this.Page.ClientScript.GetCallbackEventReference(this, num8.ToString(), "AjaxPagerCallBack", null)));
                builder.AppendFormat("<a id='{0}' href=\"javascript:{1}\">下一页</a>", this.UniqueID, this.Page.ClientScript.GetCallbackEventReference(this, num9.ToString(), "AjaxPagerCallBack", null));
            }
            if ((this.PageIndex > 10) && ((this.TotalPage - this.PageIndex) < num3))
            {
                num8 = num4 * this.BarSize;
                builder.Insert(0, string.Format("<a id='{0}' href=\"javascript:{1}\">上一页</a>", this.UniqueID, this.Page.ClientScript.GetCallbackEventReference(this, num8.ToString(), "AjaxPagerCallBack", null)));
            }
            builder.AppendFormat(" {0}", str);
            return builder.ToString();
        }




       



        protected override void LoadViewState(object savedState)
        {
            Triplet triplet = savedState as Triplet;
            this.TotalRecord = Convert.ToInt32(triplet.Third);
            this.Info = triplet.Second as PageInfo;
            base.LoadViewState(triplet.First);
        }



        protected override void RenderContents(HtmlTextWriter writer)
        {
            writer.Write(this.GetContents());
            base.RenderContents(writer);
        }




        protected override object SaveViewState()
        {
            Triplet triplet = new Triplet();
            triplet.First = base.SaveViewState();
            triplet.Second = this.Info;
            triplet.Third = this.TotalRecord;
            return triplet;
        }




        [DefaultValue("#FFFFFF"), Description("分页背景色"), Bindable(true), Category("外观")]
        public string BarBackGroundColor
        {
            get
            {
                return this._BarBackGroundColor;
            }
            set
            {
                this._BarBackGroundColor = value;
            }
        }



        [Description("分页条当前页数字颜色"), DefaultValue("#EEEEEE"), Category("外观"), Bindable(true)]
        public string BarCurrentColor
        {
            get
            {
                return this._BarCurrentColor;
            }
            set
            {
                this._BarCurrentColor = value;
            }
        }



        [DefaultValue("Navy"), Category("外观"), Description("分页条链接数字颜色"), Bindable(true)]
        public string BarLinkColor
        {
            get
            {
                return this._BarLinkColor;
            }
            set
            {
                this._BarLinkColor = value;
            }
        }



        [DefaultValue(10), Category("行为"), Description("Bar的大小")]
        public int BarSize
        {
            get
            {
                return this._ItemSize;
            }
            set
            {
                foreach (char ch in Convert.ToString(value))
                {
                    if (!char.IsNumber(ch))
                    {
                        this._ItemSize = 10;
                        break;
                    }
                }
                this._ItemSize = value;
            }
        }



        public PageInfo Info
        {
            get
            {
                return this._Info;
            }
            set
            {
                this._Info = value;
            }
        }



        [DefaultValue(1), Description("当前页值"), Category("行为")]
        public int PageIndex
        {
            get
            {
                return this._CurrentIndex;
            }
            set
            {
                this._CurrentIndex = value;
            }
        }



        [Category("行为"), DefaultValue(0), Description("总页数")]
        public int TotalPage
        {
            get
            {
                return this._TotalPage;
            }
        }



        [DefaultValue(50), Description("总记录数"), Category("行为")]
        public int TotalRecord
        {
            get
            {
                return this._TotalRecord;
            }
            set
            {
                this._TotalRecord = value;
            }
        }


        #region ICallbackEventHandler 成员

        string ICallbackEventHandler.GetCallbackResult()
        {
            StringBuilder sb = new StringBuilder();
            StringWriter writer = new StringWriter(sb);
            this.getRpt().RenderControl(new HtmlTextWriter(writer));
            return (this.GetContents() + "[_]" + sb.ToString());
        }

        void ICallbackEventHandler.RaiseCallbackEvent(string eventArgument)
        {
            int num = int.Parse(eventArgument);
            this._CurrentIndex = num;
            this.BindData();
        }

        #endregion
    }
}