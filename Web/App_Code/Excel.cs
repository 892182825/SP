using System;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using System.IO;

	/// <summary>
	/// Excel 的摘要说明。
	/// </summary>
	public class Excel
	{
		public Excel()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
        /// <summary>
        /// 导出excel——ds2012——tianfeng
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="title"></param>
        /// <param name="colsName"></param>
        /// <returns></returns>
		public static System.Text.StringBuilder GetExcelTable(System.Data.DataTable dt,string title,string[] colsName)
		{
			System.Text.StringBuilder sbexcel=new System.Text.StringBuilder();
			sbexcel.Append("<table border='1' width='100%'");
			
			for(int k=0;k<dt.Rows.Count;k++)
			{
				if(k%30000==0)
				{
                    sbexcel.Append("<tr style='height:30px'><td colspan='" + colsName.Length + "'></td></tr>");
                    sbexcel.Append("<tr style='height:30px'><td colspan='" + colsName.Length + "' align='center' style='font-size:11pt;font-weight:bold'>" + title + "</td></tr>");
					sbexcel.Append("<tr>");

                    for (int i = 0; i < colsName.Length; i++)
					{
						sbexcel.Append("<th style='background-color:rgb(223,223,223);font-size:10pt' align='center'>"+colsName[i].Split('=')[1] +"</th>");
					}

					sbexcel.Append("</tr>");
				}
				
				sbexcel.Append("<tr>");
                for (int i = 0; i < colsName.Length; i++)
				{
                    //font-size:10pt;vnd.ms-excel.numberformat:@  解决科学计数法
                    sbexcel.Append("<td align='center' style='font-size:10pt; font-size:10pt;vnd.ms-excel.numberformat:@'>" + dt.Rows[k][colsName[i].Split('=')[0]] + "</td>");
				}
				sbexcel.Append("</tr>");
			}

			sbexcel.Append("</table>");

			return sbexcel;
		}
        ///// <summary>
        ///// ds2012
        ///// 循环遍历DataTable数据，导出Excel
        ///// </summary>
        ///// <param name="dt"></param>
        ///// <param name="title"></param>
        ///// <param name="colsName"></param>
        //public static void OutToExcel(System.Data.DataTable dt, string title, string[] colsName)
        //{
        //    System.Text.StringBuilder sbexcel = new System.Text.StringBuilder();
        //    sbexcel.Append("<table border='1' width='100%'");
            
        //    for (int k = 0; k < dt.Rows.Count; k++)
        //    {
        //        if (k % 30000 == 0)
        //        {
        //            sbexcel.Append("<tr style='height:30px'><td colspan='" + colsName.Length + "'></td></tr>");
        //            sbexcel.Append("<tr style='height:30px'><td colspan='" + colsName.Length + "' align='center' style='font-size:11pt;font-weight:bold'>" + title + "</td></tr>");
        //            sbexcel.Append("<tr>");

        //            for (int i = 0; i < colsName.Length; i++)
        //            {
        //                sbexcel.Append("<th style='background-color:rgb(223,223,223);font-size:10pt' align='center'>&nbsp;" + colsName[i].Split('=')[1] + "&nbsp;</th>");
        //            }

        //            sbexcel.Append("</tr>");
        //        }

        //        sbexcel.Append("<tr>");
        //        for (int i = 0; i < colsName.Length; i++)
        //        {
        //            sbexcel.Append("<td align='center' style='font-size:10pt'>" + dt.Rows[k][colsName[i].Split('=')[0]]  + "</td>");
        //        }
        //        sbexcel.Append("</tr>");
        //    }

        //    sbexcel.Append("</table>");

        //    System.Web.HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=FileName.xls");
        //    //System.Web.HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("GB2312");
        //    System.Web.HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("utf-8");
        //    System.Web.HttpContext.Current.Response.Write(sbexcel.ToString());
        //    System.Web.HttpContext.Current.Response.Flush();
        //    System.Web.HttpContext.Current.Response.End();
        //}

        public static void OutToExcel1(System.Data.DataTable dt, string title, string[] colsName)
        {
            System.Text.StringBuilder sbexcel = new System.Text.StringBuilder();
            sbexcel.Append("<table border='1' width='100%'");

            for (int k = 0; k < dt.Rows.Count; k++)
            {
                if (k % 30000 == 0)
                {
                    sbexcel.Append("<tr style='height:30px'><td colspan='" + colsName.Length + "'></td></tr>");
                    sbexcel.Append("<tr style='height:30px'><td colspan='" + colsName.Length + "' align='center' style='font-size:11pt;font-weight:bold'>" + title + "</td></tr>");
                    sbexcel.Append("<tr>");

                    for (int i = 0; i < colsName.Length; i++)
                    {
                        sbexcel.Append("<th style='background-color:rgb(223,223,223);font-size:10pt' align='center'>" + colsName[i].Split('=')[1] + "</th>");
                    }

                    sbexcel.Append("</tr>");
                }

                sbexcel.Append("<tr>");
                for (int i = 0; i < colsName.Length; i++)
                {
                    if (dt.Columns[colsName[i].Split('=')[0]].DataType == typeof(string))
                    {
                        sbexcel.Append("<td align='center' style='font-size:10pt;vnd.ms-excel.numberformat:@'>" + dt.Rows[k][colsName[i].Split('=')[0]] + "</td>");
                    }
                    else
                    {
                        sbexcel.Append("<td align='center' style='font-size:10pt'>" + dt.Rows[k][colsName[i].Split('=')[0]] + "</td>");
                    }
                }
                sbexcel.Append("</tr>");
            }

            sbexcel.Append("</table>");

            System.Web.HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=FileName.xls");
            System.Web.HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("GB2312");
            //System.Web.HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("utf-8");
            System.Web.HttpContext.Current.Response.Write(sbexcel.ToString());
            System.Web.HttpContext.Current.Response.Flush();
            System.Web.HttpContext.Current.Response.End();
        }

        /// <summary>
        /// ds2012
        /// 循环遍历DataTable数据，导出Excel
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="title"></param>
        /// <param name="colsName"></param>
        public static void OutToExcel( System.Data.DataTable dt, string title, string[] colsName)
        {
            System.Web.UI.Page page = new System.Web.UI.Page();
            System.Data.DataTable ddt = new System.Data.DataTable();
            for (int i = 0; i < colsName.Length; i++)
            {
                DataColumn dc = new DataColumn(colsName[i].Split('=')[1]);
                ddt.Columns.Add(dc);
            }
            for (int k = 0; k < dt.Rows.Count; k++)
            {
                DataRow dr = ddt.NewRow();
                for (int i = 0; i < colsName.Length; i++)
                {
                    dr[colsName[i].Split('=')[1]] = dt.Rows[k][colsName[i].Split('=')[0]];
                }
                ddt.Rows.Add(dr);
            }

            System.Web.UI.WebControls.DataGrid dataGrid = new System.Web.UI.WebControls.DataGrid();
            dataGrid.DataSource = ddt.DefaultView;
            dataGrid.AllowPaging = false;
            dataGrid.HeaderStyle.BackColor = System.Drawing.Color.Gray;
            dataGrid.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            dataGrid.HeaderStyle.Font.Bold = true;
            dataGrid.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
            dataGrid.DataBind();
            System.IO.StringWriter tw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
            dataGrid.RenderControl(hw);

            if (!Directory.Exists(page.Server.MapPath("../UserExcel/")))
            {
                Directory.CreateDirectory(page.Server.MapPath("../UserExcel/"));
            }
            string fullpath = page.Server.MapPath("../UserExcel/FileName.xls");

            System.IO.StreamWriter sw = System.IO.File.CreateText(fullpath);
           
            sw.Write(tw.ToString());
            sw.Close();

            string where = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
            //压缩后的目标文件
            string zipPath = page.Server.MapPath("../UserExcel/") + where + ".rar";
            ZipClass Zc = new ZipClass();
            string err = "";
            Zc.ZipFile(fullpath, zipPath, out err);

            //删除压缩前的文件
            System.IO.File.Delete(fullpath);

            System.IO.FileInfo file = new System.IO.FileInfo(zipPath);

            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.ClearContent();
            System.Web.HttpContext.Current.Response.ClearHeaders();
            System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(file.Name).Replace('+', ' '));
            System.Web.HttpContext.Current.Response.AddHeader("Content-Length", file.Length.ToString());
            System.Web.HttpContext.Current.Response.AddHeader("Content-Transfer-Encoding", "binary");
            System.Web.HttpContext.Current.Response.ContentType = "application/zip";
            System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
            System.Web.HttpContext.Current.Response.WriteFile(file.FullName);
            System.Web.HttpContext.Current.Response.Flush();
            System.Web.HttpContext.Current.Response.End();
        }
	}

