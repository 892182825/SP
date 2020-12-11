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
using DAL;

/// <summary>
///UserControlBase 的摘要说明
/// </summary>
public class UserControlBase : System.Web.UI.UserControl
{
		//		protected string _sn;
		
		public UserControlBase() : base ()
		{

		}

        //protected override void RenderChildren(System.Web.UI.HtmlTextWriter writer)
        //{
        //    ScanControls(this,0);		//赋值文件名后开始扫描
        //    base.RenderChildren (writer);
        //}

        //private string _FileName;
        //private string _AspxFileName;
        //public string FileName
        //{
        //    get {return _FileName;}
        //    set 
        //    {
        //        string language = System.Web.HttpContext.Current.Session["language"].ToString();
        //        _FileName = value;	
        //        _AspxFileName = (_FileName.Substring(0,_FileName.Length-3));
        //        if (System.Web.HttpContext.Current.Application["Load_"+language+"_"+_FileName]==null)	//第一次加载页面时从数据库读入 Application
        //        {
        //            System.Data.SqlClient.SqlDataReader dread = DBHelper.ExecuteReader("SELECT location,text FROM Tran2"+language+" WHERE filename='"+_FileName+"'");
        //            System.Collections.Hashtable htb = (System.Collections.Hashtable)System.Web.HttpContext.Current.Application["Dict_"+language];
        //            while(dread.Read())
        //            {
        //                htb.Add(dread.GetString(0),dread.GetString(1));
        //            }
        //            dread.Close();
        //            System.Web.HttpContext.Current.Application.UnLock();
        //            System.Web.HttpContext.Current.Application.Lock();
        //            System.Web.HttpContext.Current.Application["Dict_"+language] = htb;
        //            System.Web.HttpContext.Current.Application["Load_"+language+"_"+_FileName] = true;
        //            System.Web.HttpContext.Current.Application.UnLock();
        //        }
        //        if (System.Web.HttpContext.Current.Application["Load_"+language+"_"+_AspxFileName]==null)	//第一次加载页面时从数据库读入 Application
        //        {
        //            System.Data.SqlClient.SqlDataReader dread = DBHelper.ExecuteReader("SELECT location,text FROM Tran2"+language+" WHERE filename='"+_AspxFileName+"'");
        //            System.Collections.Hashtable htb = (System.Collections.Hashtable)System.Web.HttpContext.Current.Application["Dict_"+language];
        //            while(dread.Read())
        //            {
        //                htb.Add(dread.GetString(0),dread.GetString(1));
        //            }
        //            dread.Close();
        //            System.Web.HttpContext.Current.Application.UnLock();
        //            System.Web.HttpContext.Current.Application.Lock();
        //            System.Web.HttpContext.Current.Application["Dict_"+language] = htb;
        //            System.Web.HttpContext.Current.Application["Load_"+language+"_"+_AspxFileName] = true;
        //            System.Web.HttpContext.Current.Application.UnLock();
        //        }
        //    }
        //}
        //private void ScanControls(System.Web.UI.Control con,int ScanLayer) 
        //{
        //    TranControls(con);	//翻译当前控件
        //    if (ScanLayer>=6 || (!con.HasControls()))	//当扫描的控件层次大于等于6或当前控件无子控件则不继续递归
        //    {
        //        return;
        //    }
        //    else
        //    {
        //        for (int i=0;i<con.Controls.Count;i++)
        //        {
        //            ScanControls(con.Controls[i],ScanLayer+1);
        //        }
        //    }
        //}

        //private void TranControls(System.Web.UI.Control con)
        //{
        //    string TranString = "";
        //    switch(con.GetType().ToString())
        //    {
        //        case "System.Web.UI.WebControls.DataGrid":
        //            TranDataGrid((System.Web.UI.WebControls.DataGrid)con);
        //            break;
        //        case "System.Web.UI.WebControls.Button":
        //            TranString = GetTran(_FileName+"_"+con.ID);
        //            if (TranString != "")
        //            {
        //                ((System.Web.UI.WebControls.Button)con).Text = TranString ;
        //            }
        //            break;
        //        case "System.Web.UI.WebControls.TextBox":
        //            TranString = GetTran(_FileName+"_"+con.ID);
        //            if (TranString != "")
        //            {
        //                ((System.Web.UI.WebControls.TextBox)con).Text = TranString ;
        //            }
        //            break;
        //        case "System.Web.UI.WebControls.Label":
        //            TranString = GetTran(_FileName+"_"+con.ID);
        //            if (TranString != "")
        //            {
        //                ((System.Web.UI.WebControls.Label)con).Text = TranString ;
        //            }
        //            break;
        //        case "System.Web.UI.WebControls.LinkButton":
        //            TranString = GetTran(_FileName+"_"+con.ID);
        //            if (TranString != "")
        //            {
        //                ((System.Web.UI.WebControls.LinkButton)con).Text = TranString;
        //            }
        //            break;
        //        case "System.Web.UI.WebControls.HyperLink":
        //            TranString = GetTran(_FileName+"_"+con.ID);
        //            if (TranString != "")
        //            {
        //                ((System.Web.UI.WebControls.HyperLink)con).Text = TranString;
        //            }
        //            break;
        //        case "System.Web.UI.WebControls.DropDownList":
        //            TranDropDownList((System.Web.UI.WebControls.DropDownList)con);
        //            break;
        //        case "System.Web.UI.WebControls.CheckBox":
        //            TranString = GetTran(_FileName+"_"+con.ID);
        //            if (TranString != "")
        //            {
        //                ((System.Web.UI.WebControls.CheckBox)con).Text = TranString;
        //            }
        //            break;
        //        case "System.Web.UI.WebControls.CheckBoxList":
        //            TranCheckBoxList((System.Web.UI.WebControls.CheckBoxList)con);
        //            break;
        //        case "System.Web.UI.WebControls.RadioButton":
        //            TranString = GetTran(_FileName+"_"+con.ID);
        //            if (TranString != "")
        //            {
        //                ((System.Web.UI.WebControls.RadioButton)con).Text = TranString;
        //            }
        //            break;
        //        case "System.Web.UI.WebControls.RadioButtonList":
        //            TranRadioButtonList((System.Web.UI.WebControls.RadioButtonList)con);
        //            break;
        //        case "System.Web.UI.WebControls.Image":
        //            TranImage((System.Web.UI.WebControls.Image)con);
        //            break;
        //        case "System.Web.UI.WebControls.ImageButton":
        //            TranImageButton((System.Web.UI.WebControls.ImageButton)con);
        //            break;
        //        case "System.Web.UI.WebControls.RequiredFieldValidator":
        //            TranString = GetTran(_FileName+"_"+con.ID);
        //            if (TranString != "")
        //            {
        //                ((System.Web.UI.WebControls.RequiredFieldValidator)con).ErrorMessage = TranString;
        //            }
        //            break;
        //        case "System.Web.UI.WebControls.RangeValidator":
        //            TranString = GetTran(_FileName+"_"+con.ID);
        //            if (TranString != "")
        //            {
        //                ((System.Web.UI.WebControls.RangeValidator)con).ErrorMessage = TranString;
        //            }
        //            break;
        //        case "System.Web.UI.WebControls.RegularExpressionValidator":
        //            TranString = GetTran(_FileName+"_"+con.ID);
        //            if (TranString != "")
        //            {
        //                ((System.Web.UI.WebControls.RegularExpressionValidator)con).ErrorMessage = TranString;
        //            }
        //            break;
        //        case "System.Web.UI.WebControls.Literal":
        //            TranString = GetTran(_FileName+"_"+con.ID);
        //            if (TranString != "")
        //            {
        //                ((System.Web.UI.WebControls.Literal)con).Text = TranString;
        //            }
        //            break;
        //    }
        //}
        //public string GetTran(string Text)
        //{
        //    string language = System.Web.HttpContext.Current.Session["language"].ToString();
        //    System.Collections.Hashtable htb = (System.Collections.Hashtable)System.Web.HttpContext.Current.Application["Dict_"+language];
        //    if (htb.Contains(Text))
        //    {
        //        return htb[Text].ToString();	//返回原字符串
        //    }
        //    else
        //    {
        //        return "";	//返回空字符串
        //    }
        //}
        //public string GetTran(int line)
        //{
        //    string Text = _FileName+"_"+line;
        //    string language = System.Web.HttpContext.Current.Session["language"].ToString();
        //    System.Collections.Hashtable htb = (System.Collections.Hashtable)System.Web.HttpContext.Current.Application["Dict_"+language];
        //    if (htb.Contains(Text))
        //    {
        //        return htb[Text].ToString();	//返回原字符串
        //    }
        //    else
        //    {
        //        return "";	//返回空字符串
        //    }
        //}
        //private void TranDropDownList(System.Web.UI.WebControls.DropDownList drop)
        //{
        //    for (int i=0;i<drop.Items.Count;i++)
        //    {
        //        string TranString = GetTran(_FileName+"_"+drop.ID+"_"+i.ToString());
        //        if (TranString != "")
        //        {
        //            drop.Items[i].Text = TranString;
        //        }
        //    }
        //}
        //private void TranRadioButtonList(System.Web.UI.WebControls.RadioButtonList rbl)
        //{
        //    for (int i=0;i<rbl.Items.Count;i++)
        //    {
        //        string TranString = GetTran(_FileName+"_"+rbl.ID+"_"+i.ToString());
        //        if (TranString != "")
        //        {
        //            rbl.Items[i].Text = TranString;
        //        }
        //    }
        //}
        //private void TranCheckBoxList(System.Web.UI.WebControls.CheckBoxList cbl)
        //{
        //    for (int i=0;i<cbl.Items.Count;i++)
        //    {
        //        string TranString = GetTran(_FileName+"_"+cbl.ID+"_"+i.ToString());
        //        if (TranString != "")
        //        {
        //            cbl.Items[i].Text = TranString;
        //        }
        //    }
        //}
        //private void TranDataGrid(System.Web.UI.WebControls.DataGrid dg)
        //{
        //    if (dg.HasControls())
        //    {
        //        int HeaderIndex = (((System.Web.UI.WebControls.DataGridItem)dg.Controls[0].Controls[0]).ItemType == System.Web.UI.WebControls.ListItemType.Header)?0:1;
        //        //翻译抬头
        //        for (int i=0;i<dg.Columns.Count;i++)
        //        {
        //            string TranString = GetTran(_FileName+"_"+dg.ID+"_"+i.ToString());
        //            switch(dg.Columns[i].GetType().ToString())
        //            {
        //                case "System.Web.UI.WebControls.BoundColumn":
        //                    if (TranString != "")
        //                    {
        //                        //((System.Web.UI.WebControls.BoundColumn)dg.Columns[i]).HeaderText = TranString;
        //                        ((System.Web.UI.WebControls.TableCell)dg.Controls[0].Controls[HeaderIndex].Controls[i]).Text = TranString;
        //                    }
        //                    break;
        //                case "System.Web.UI.WebControls.TemplateColumn":
        //                    if (TranString != "")
        //                    {
        //                        //((System.Web.UI.WebControls.TemplateColumn)dg.Columns[i]).HeaderText = TranString;
        //                        ((System.Web.UI.WebControls.TableCell)dg.Controls[0].Controls[HeaderIndex].Controls[i]).Text = TranString;
        //                    }
        //                    break;
        //                case "System.Web.UI.WebControls.HyperLinkColumn":
        //                    if (TranString != "")
        //                    {
        //                        //((System.Web.UI.WebControls.HyperLinkColumn)dg.Columns[i]).HeaderText = TranString;
        //                        ((System.Web.UI.WebControls.TableCell)dg.Controls[0].Controls[HeaderIndex].Controls[i]).Text = TranString;
        //                    }
        //                    break;
        //                case "System.Web.UI.WebControls.ButtonColumn":
        //                    if (TranString != "")
        //                    {
        //                        //((System.Web.UI.WebControls.ButtonColumn)dg.Columns[i]).HeaderText = TranString;
        //                        ((System.Web.UI.WebControls.TableCell)dg.Controls[0].Controls[HeaderIndex].Controls[i]).Text = TranString;
        //                    }
        //                    break;
        //                default:
        //                    break;
        //            }
        //        }
        //    }
        //}
        //private void TranImage(System.Web.UI.WebControls.Image img)
        //{
        //    string TranString = GetTran(_FileName+"_"+img.ID);
        //    if (TranString != "")
        //    {
        //        img.ImageUrl = TranString;
        //    }
        //}
        //private void TranImageButton(System.Web.UI.WebControls.ImageButton imgbtn)
        //{
        //    string TranString = GetTran(_FileName+"_"+imgbtn.ID);
        //    if (TranString != "")
        //    {
        //        imgbtn.ImageUrl = TranString;
        //    }
        //}
        //public string GetAspxTran(string Location)
        //{
        //    string Text = _AspxFileName+"_"+Location;
        //    string language = System.Web.HttpContext.Current.Session["language"].ToString();
        //    System.Collections.Hashtable htb = (System.Collections.Hashtable)System.Web.HttpContext.Current.Application["Dict_"+language];
        //    if (htb.Contains(Text))
        //    {
        //        return htb[Text].ToString();	//返回原字符串
        //    }
        //    else
        //    {
        //        return "";	//返回空字符串
        //    }
        //}
}
