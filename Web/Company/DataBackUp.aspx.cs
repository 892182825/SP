using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

//Add Namespace
using BLL;
using System.Text;
using Model.Other;
using BLL.other.Company;
using BLL.CommonClass;
using Encryption;

/*
 * 创建者：     汪  华
 * 创建时间：   2009-09-10
 * 对应菜单：   系统管理->数据备份
 */

public partial class Company_DataBackUp : BLL.TranslationBase
{
    protected string msg;

    /// <summary>
    /// 文件名
    /// </summary>
    protected static string fileName="";

    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        ///检查相应权限
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission (EnumCompanyPermission.SystemBackup);

        if (!IsPostBack)
        {
            txtMemberBeginTime.Text = CommonDataBLL.GetDateBegin();
            txtMemberEndTime.Text = CommonDataBLL.GetDateEnd();
            txtOrderBeginTime.Text = CommonDataBLL.GetDateBegin();
            txtOrderEndTime.Text = CommonDataBLL.GetDateEnd();
            txtBegin.Text = CommonDataBLL.GetDateBegin();
            txtEnd.Text = CommonDataBLL.GetDateEnd();


            ///绑定期数列表
            BindExpectNumList();
            BindBackupExpectNumList();
            BindBackupExpectddlqishu();

            ///隐藏选择范围，用户选择了指定方式我们才显示            
            trDate.Visible = false;
            trExpectNum.Visible = false;
            trDataBackupDate.Visible = false;
            trDataBackupExpectNum.Visible = false;

            tr1.Visible = false;
            tr2.Visible = false;
        }
        //Translations();
    }
    private void Translations()
    {
        this.TranControls(this.ddlOrderDetail,
               new string[][]{
                    new string []{"004074","请选择备份方式"},
                    new string []{"004080", "按日期备份"},
                    new string []{"004078", "按期数"}});
        this.TranControls(this.ddlMemberDetails,
                    new string[][]{
                    new string []{"004074","请选择备份方式"},
                    new string []{"004080", "按日期备份"},
                    new string []{"004078", "按期数"}});
        this.TranControls(this.ddlfs,
                    new string[][]{
                    new string []{"004074","请选择备份方式"},
                    new string []{"004080", "按日期备份"},
                    new string []{"004078", "按期数"}});
        this.TranControls(this.btnToBackupDatabase, new string[][] { new string[] { "004072", "转到数据库备份" } });
        this.TranControls(this.btnBackupMemberDetails, new string[][] { new string[] { "006709", "备 份" } });
        this.TranControls(this.btnBackupOrderDetail, new string[][] { new string[] { "006709", "备 份" } });
        this.TranControls(this.btnBackupMemberInfo, new string[][] { new string[] { "006709", "备 份" } });
    }

    /// <summary>
    /// 绑定期数列表
    /// </summary>
    private void BindExpectNumList()
    {
        int currentExpectNum = Convert.ToInt32(Session["nowqishu"]);
        DataTable dt;
        ///从结算表中获取期数
        dt = DataBackupBLL.GetExpectNumFromConfig();
        ddlExpectNum.DataSource = dt;
        ddlExpectNum.DataTextField = "ExpectNum";
        ddlExpectNum.DataValueField = "ExpectNum";
        ddlExpectNum.DataBind();
		
        ddlExpectNum.SelectedIndex = -1;
        ///选中当前期数
        foreach(ListItem item in ddlExpectNum.Items )
        {
            if ( item.Value  == currentExpectNum.ToString() )
            {
                item.Selected = true;
                break;
            }
        }
    }

    /// <summary>
    ///绑定期数列表 
    /// </summary>
    private void BindBackupExpectNumList()
    {
        int currentExpectNum = Convert.ToInt32(Session["nowqishu"]);
        DataTable dt;	
        ///从结算表中获取期数
        dt = DataBackupBLL.GetExpectNumFromConfig();
        ddlDataBackupExpectNum.DataSource = dt;
        ddlDataBackupExpectNum.DataTextField = "ExpectNum";
        ddlDataBackupExpectNum.DataValueField = "ExpectNum";
        ddlDataBackupExpectNum.DataBind();

        ddlExpectNum.SelectedIndex = -1;
        ///选中当前期数
        foreach(ListItem item in ddlExpectNum.Items )
        {
            if ( item.Value  == currentExpectNum.ToString() )
            {
                item.Selected = true;
                break;
            }
        }
        Translations();
    }

    /// <summary>
    ///绑定期数列表 
    /// </summary>
    private void BindBackupExpectddlqishu()
    {
        int currentExpectNum = Convert.ToInt32(Session["nowqishu"]);
        DataTable dt;
        ///从结算表中获取期数
        dt = DataBackupBLL.GetExpectNumFromConfig();
        ddlqishu.DataSource = dt;
        ddlqishu.DataTextField = "ExpectNum";
        ddlqishu.DataValueField = "ExpectNum";
        ddlqishu.DataBind();

        ddlExpectNum.SelectedIndex = -1;
        ///选中当前期数
        foreach (ListItem item in ddlExpectNum.Items)
        {
            if (item.Value == currentExpectNum.ToString())
            {
                item.Selected = true;
                break;
            }
        }
        Translations();
    }

     ///<summary>
     ///备份会员基本信息
     ///</summary>
    private void BackupMemberInfo()
    {    
        ///把数据从会员信息表中复制到会员备份基本信息表中
        DataBackupBLL.AddAllDataFromMemberInfo();

        DataTable dt, oldDt;
        dt=new DataTable();
        ///从会员备份基本信息表获取所有的信息            
        oldDt = DataBackupBLL.GetAllInfoFromBackupMemberInfo();
        //Clone the datatable structure
        dt = oldDt.Clone();
        foreach (DataColumn col in dt.Columns)
        {
            if (col.ColumnName == "Sex")
            { 
                //Update the column data type
                col.DataType=typeof(String);
            }
        }

        for (int i = 0; i < oldDt.Rows.Count; i++)
        {
            DataRow newRow = dt.NewRow();
            newRow.ItemArray = oldDt.Rows[i].ItemArray;
            dt.Rows.Add(newRow);
            
            //By WangHua 2010-02-02(等到数据完善后才取消注释)
            dt.Rows[i]["Name"] = Encryption.Encryption.GetDecipherName(Convert.ToString(dt.Rows[i]["Name"]));
            dt.Rows[i]["HomeTele"] = Encryption.Encryption.GetDecipherTele(Convert.ToString(dt.Rows[i]["HomeTele"]));
            dt.Rows[i]["OfficeTele"] = Encryption.Encryption.GetDecipherTele(Convert.ToString(dt.Rows[i]["OfficeTele"]));
            dt.Rows[i]["MobileTele"] = Encryption.Encryption.GetDecipherTele(Convert.ToString(dt.Rows[i]["MobileTele"]));
            dt.Rows[i]["FaxTele"] = Encryption.Encryption.GetDecipherTele(Convert.ToString(dt.Rows[i]["FaxTele"]));
            dt.Rows[i]["Address"] = Encryption.Encryption.GetDecipherAddress(Convert.ToString(dt.Rows[i]["Address"]));
            
            if (Convert.ToString(dt.Rows[i]["Sex"]) == "1")
            {
                dt.Rows[i]["Sex"] = GetTran("000094", "男");
            }                    

            else
            {
                dt.Rows[i]["Sex"] = GetTran("000095", "女");
            }
        }

        Excel.OutToExcel(dt, GetTran("004172", "会员信息表"), new string[] { "Number="+GetTran("000024","编号"), "ExpectNum="+GetTran("000045","期数"), 
            "StoreId="+GetTran("000150","店铺编号"), "Name="+GetTran("000025","姓名"), "PetName="+GetTran("001400","昵称"), "RegisterDate="+GetTran("000057","注册日期"), 
            "Birthday="+GetTran("000105","生日"), "Sex="+GetTran("000085","性别"), "HomeTele="+GetTran("000065","家庭电话"), "OfficeTele="+GetTran("000044","办公电话"),
            "MobileTele="+GetTran("000069","移动电话"), "FaxTele="+GetTran("000643","传真"), "Country="+GetTran("002157","国家"), "Province="+GetTran("000109","省份"),
            "City="+GetTran("000110","城市"), "Address="+GetTran("000072","地址"), "PostalCode="+GetTran("000073","邮编"), "Email="+GetTran("000330","电子邮箱") });

        ///弹出成功提示
        msg = "<script language='javascript'>alert('" + GetTran("004097", "备份会员基本信息成功") + "！')</script>";
    }

    ///<summary>
    ///备份会员基本信息
    ///</summary>
    private void BackupMemberInfo(DateTime beginDate, DateTime endDate)
    {
        ///把数据从会员信息表中复制到会员备份基本信息表中
        DataBackupBLL.AddAllDataFromMemberInfo(beginDate,endDate);

        DataTable dt, oldDt;
        dt = new DataTable();
        ///从会员备份基本信息表获取所有的信息            
        oldDt = DataBackupBLL.GetAllInfoFromBackupMemberInfo();
        //Clone the datatable structure
        dt = oldDt.Clone();
        foreach (DataColumn col in dt.Columns)
        {
            if (col.ColumnName == "Sex")
            {
                //Update the column data type
                col.DataType = typeof(String);
            }
        }

        for (int i = 0; i < oldDt.Rows.Count; i++)
        {
            DataRow newRow = dt.NewRow();
            newRow.ItemArray = oldDt.Rows[i].ItemArray;
            dt.Rows.Add(newRow);

            //By WangHua 2010-02-02(等到数据完善后才取消注释)
            dt.Rows[i]["Name"] = Encryption.Encryption.GetDecipherName(Convert.ToString(dt.Rows[i]["Name"]));
            dt.Rows[i]["HomeTele"] = Encryption.Encryption.GetDecipherTele(Convert.ToString(dt.Rows[i]["HomeTele"]));
            dt.Rows[i]["OfficeTele"] = Encryption.Encryption.GetDecipherTele(Convert.ToString(dt.Rows[i]["OfficeTele"]));
            dt.Rows[i]["MobileTele"] = Encryption.Encryption.GetDecipherTele(Convert.ToString(dt.Rows[i]["MobileTele"]));
            dt.Rows[i]["FaxTele"] = Encryption.Encryption.GetDecipherTele(Convert.ToString(dt.Rows[i]["FaxTele"]));
            dt.Rows[i]["Address"] = Encryption.Encryption.GetDecipherAddress(Convert.ToString(dt.Rows[i]["Address"]));

            if (Convert.ToString(dt.Rows[i]["Sex"]) == "1")
            {
                dt.Rows[i]["Sex"] = GetTran("000094", "男");
            }

            else
            {
                dt.Rows[i]["Sex"] = GetTran("000095", "女");
            }
        }

        Excel.OutToExcel(dt, GetTran("004172", "会员信息表"), new string[] { "Number="+GetTran("000024","编号"), "ExpectNum="+GetTran("000045","期数"), 
            "StoreId="+GetTran("000150","店铺编号"), "Name="+GetTran("000025","姓名"), "PetName="+GetTran("001400","昵称"), "RegisterDate="+GetTran("000057","注册日期"), 
            "Birthday="+GetTran("000105","生日"), "Sex="+GetTran("000085","性别"), "HomeTele="+GetTran("000065","家庭电话"), "OfficeTele="+GetTran("000044","办公电话"),
            "MobileTele="+GetTran("000069","移动电话"), "FaxTele="+GetTran("000643","传真"), "Country="+GetTran("002157","国家"), "Province="+GetTran("000109","省份"),
            "City="+GetTran("000110","城市"), "Address="+GetTran("000072","地址"), "PostalCode="+GetTran("000073","邮编"), "Email="+GetTran("000330","电子邮箱") });

        ///弹出成功提示
        msg = "<script language='javascript'>alert('" + GetTran("004097", "备份会员基本信息成功") + "！')</script>";
    }

    ///<summary>
    ///备份会员基本信息
    ///</summary>
    private void BackupMemberInfo(int qishu)
    {
        ///把数据从会员信息表中复制到会员备份基本信息表中
        DataBackupBLL.AddAllDataFromMemberInfo(qishu);

        DataTable dt, oldDt;
        dt = new DataTable();
        ///从会员备份基本信息表获取所有的信息            
        oldDt = DataBackupBLL.GetAllInfoFromBackupMemberInfo();
        //Clone the datatable structure
        dt = oldDt.Clone();
        foreach (DataColumn col in dt.Columns)
        {
            if (col.ColumnName == "Sex")
            {
                //Update the column data type
                col.DataType = typeof(String);
            }
        }

        for (int i = 0; i < oldDt.Rows.Count; i++)
        {
            DataRow newRow = dt.NewRow();
            newRow.ItemArray = oldDt.Rows[i].ItemArray;
            dt.Rows.Add(newRow);

            //By WangHua 2010-02-02(等到数据完善后才取消注释)
            dt.Rows[i]["Name"] = Encryption.Encryption.GetDecipherName(Convert.ToString(dt.Rows[i]["Name"]));
            dt.Rows[i]["HomeTele"] = Encryption.Encryption.GetDecipherTele(Convert.ToString(dt.Rows[i]["HomeTele"]));
            dt.Rows[i]["OfficeTele"] = Encryption.Encryption.GetDecipherTele(Convert.ToString(dt.Rows[i]["OfficeTele"]));
            dt.Rows[i]["MobileTele"] = Encryption.Encryption.GetDecipherTele(Convert.ToString(dt.Rows[i]["MobileTele"]));
            dt.Rows[i]["FaxTele"] = Encryption.Encryption.GetDecipherTele(Convert.ToString(dt.Rows[i]["FaxTele"]));
            dt.Rows[i]["Address"] = Encryption.Encryption.GetDecipherAddress(Convert.ToString(dt.Rows[i]["Address"]));

            if (Convert.ToString(dt.Rows[i]["Sex"]) == "1")
            {
                dt.Rows[i]["Sex"] = GetTran("000094", "男");
            }

            else
            {
                dt.Rows[i]["Sex"] = GetTran("000095", "女");
            }
        }

        Excel.OutToExcel(dt, GetTran("004172", "会员信息表"), new string[] { "Number="+GetTran("000024","编号"), "ExpectNum="+GetTran("000045","期数"), 
            "StoreId="+GetTran("000150","店铺编号"), "Name="+GetTran("000025","姓名"), "PetName="+GetTran("001400","昵称"), "RegisterDate="+GetTran("000057","注册日期"), 
            "Birthday="+GetTran("000105","生日"), "Sex="+GetTran("000085","性别"), "HomeTele="+GetTran("000065","家庭电话"), "OfficeTele="+GetTran("000044","办公电话"),
            "MobileTele="+GetTran("000069","移动电话"), "FaxTele="+GetTran("000643","传真"), "Country="+GetTran("002157","国家"), "Province="+GetTran("000109","省份"),
            "City="+GetTran("000110","城市"), "Address="+GetTran("000072","地址"), "PostalCode="+GetTran("000073","邮编"), "Email="+GetTran("000330","电子邮箱") });

        ///弹出成功提示
        msg = "<script language='javascript'>alert('" + GetTran("004097", "备份会员基本信息成功") + "！')</script>";
    }

    private DataTable OutToExcel_MemberDetails(DataTable oldDt)
    {
        DataTable dt = new DataTable();
        dt = oldDt.Clone();
        //Update Update the column data type
        foreach(DataColumn col in dt.Columns)
        {
            if (col.ColumnName == "IsAgain")
            { 
                col.DataType=typeof(String);
            }          
        }

        for (int i = 0; i < oldDt.Rows.Count; i++)
        {
            DataRow newRow = dt.NewRow();
            newRow.ItemArray = oldDt.Rows[i].ItemArray;
            dt.Rows.Add(newRow);
            if (dt.Rows[i]["IsAgain"].ToString() == "0")
            {
                dt.Rows[i]["IsAgain"] = GetTran("004166", "未复消");
            }

            else
            {
                dt.Rows[i]["IsAgain"] = GetTran("000513", "复消");
            }
        }
        return dt;           
    }   

     ///<summary>
     ///根据期数范围备份会员明细表
     ///</summary>      
    ///<param name="expectNum">指定的期数</param>
    private void BackupMemberDetailsByExpectNum(int expectNum)
    {
        ///根据期数删除会员备份报单产品明细数据		
        DataBackupBLL.DelBackupMemberDetailsByExpectNum(expectNum);

        ///根据期数删除会员备份报单产品明细数据
        DataBackupBLL.AddBackupMemberDetailsByExpectNum(expectNum);

        //获取所有会员备份报单产品明细数据
        DataTable dt = DataBackupBLL.GetALLBackupMemberDetails();

        if (dt.Rows.Count < 0)
        {
            msg = "<script language='javascript'>alert('" + GetTran("004099", "备份会员明细成功") + "！')</script>";
            return;
        }
        else
        {

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["OrderID"] = "&nbsp;" + dt.Rows[i]["OrderID"].ToString();
            }
            Excel.OutToExcel
                (OutToExcel_MemberDetails(dt), GetTran("004155", "会员明细"), 
                    new string[] 
                        {
                            "Number="+GetTran("000024","编号"), 
                            "OrderID="+GetTran("000079","订单号"), 
                            "StoreID="+GetTran("000150","店铺编号"), 
                            "ProductID="+GetTran("000558","产品编号"), 
                            "Quantity="+GetTran("000505","数量"), 
                            "Price="+GetTran("002084","价格"), 
                            "Pv="+GetTran("000414","积分"), 
                            "ExpectNum="+GetTran("000045","期数"), 
                            "IsAgain="+GetTran("004159","是否复消"), 
                            "Remark="+GetTran("000078","备注"), 
                            "OrderDate="+GetTran("000735","订单日期") 
                        }
                        );
            ///弹出成功提示
            msg = "<script language='javascript'>alert('" + GetTran("004099", "备份会员明细成功") + "！')</script>";	
        }        
    }

         ///<summary>
         ///根据日期范围备份会员明细表
         ///</summary>
         ///<param name="beginDate">开始时间</param>
         ///<param name="endDate">结束时间</param>
        private void BackupMemberDetailsByDate(DateTime beginDate, DateTime endDate)
        {
            ///删除指定日期备份的会员报单产品明细记录
            DataBackupBLL.DelBackupMemberDetailsByDate(beginDate,endDate);            

            ///备份指定时间的会员报单产品明细记录
            DataBackupBLL.AddBackupMemberDetailsByDate(beginDate,endDate);            

            ///获取所有会员备份报单产品明细数据
            DataTable dt = DataBackupBLL.GetALLBackupMemberDetails();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["OrderID"] = "&nbsp;" + dt.Rows[i]["OrderID"].ToString();
            }
            Excel.OutToExcel(OutToExcel_MemberDetails(dt), GetTran("004155", "会员明细"),
            new string[] { "Number="+GetTran("000024","编号"), "OrderID="+GetTran("000079","订单号"), "StoreID="+GetTran("000150","店铺编号"), 
                "ProductID="+GetTran("000558","产品编号"),
                "Quantity="+GetTran("000505","数量"), "Price="+GetTran("002084","价格"), "Pv="+GetTran("000414","积分"), "ExpectNum="+GetTran("000045","期数"),
                "IsAgain="+GetTran("004159","是否复消"), "Remark="+GetTran("000078","备注"), "OrderDate="+GetTran("000735","订单日期") });          

            ///弹出成功提示
            msg = "<script language='javascript'>alert('" + GetTran("004099", "备份会员明细成功") + "！')</script>";
        }  

         ///<summary>
         ///根据期数备份专卖店购货商品明细表
         ///</summary>
         ///<param name="expectNum">指定的期数</param>
        private void BackupOrderDetailByExpectNum(int expectNum)
        {
            ///删除指定期数店铺订货产品明细记录
            DataBackupBLL.DelBackupOrderDetailByExpectNum(expectNum);

            ///备份指定期数店铺订货产品明细记录
            DataBackupBLL.AddBackupOrderDetailByExpectNum(expectNum);

            ///获取所有的店铺订货产品明细备份数据
            DataTable dt = DataBackupBLL.GetALLBackupOrderDetail();

            Excel.OutToExcel(dt, GetTran("004143", "专卖店购货商品明细"), new string[]{ "StoreOrderID="+GetTran("000079","订单号"), 
                "StoreID="+GetTran("000150","店铺编号"), "ProductID="+GetTran("000558","产品编号"), "ExpectNum="+GetTran("000045","期数"), "Quantity="+GetTran("000505","数量"),
                "NeedNumber="+GetTran("001681","应补数量"), "DumpQuantity="+GetTran("004149","冲红数量"), "Price="+GetTran("002054","产品单价"),
                "Pv="+GetTran("002057","产品积分"), "Description="+GetTran("001680","描述") });

            ///弹出成功提示
            msg = "<script language='javascript'>alert('" + GetTran("004122", "备份专卖店购货商品明细成功") + "！')</script>";
        }

         ///<summary>
         ///根据日期范围备份专卖店购货商品明细表
         ///</summary>
         ///<param name="beginDate">开始时间</param>
         ///<param name="endDate">结束时间</param>
        private void BackupOrderDetailsByDate(DateTime beginDate, DateTime endDate)
        {
            ///清空所有的店铺订货产品明细备份数据
            DataBackupBLL.ClearAllBackupOrderDetail();

            ///根据日期范围备份店铺订货产品明细信息
            DataBackupBLL.AddBackupOrderDetailByDate(beginDate, endDate); 

            ///获取所有的店铺订货产品明细备份数据
            DataTable dt = DataBackupBLL.GetALLBackupOrderDetail();

            Excel.OutToExcel(dt, GetTran("004143", "专卖店购货商品明细"), new string[] { "StoreOrderID="+GetTran("000079","订单号"),
                "StoreID="+GetTran("000150","店铺编号"), "ProductID="+GetTran("000558","产品编号"), "ExpectNum="+GetTran("000045","期数"),
                "Quantity="+GetTran("000505","数量"), "NeedNumber="+GetTran("001681","应补数量"), "DumpQuantity="+GetTran("004149","冲红数量"), 
                "Price="+GetTran("002054","产品单价"), "Pv="+GetTran("002057","产品积分"), "Description="+GetTran("001680","描述") });
         
            ///弹出成功提示
            msg = "<script language='javascript'>alert('" + GetTran("004141", "备份卖店购货商品明细成功") + "！')</script>";		
        }
        
        /// <summary>
        /// 备份所有会员基本信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBackupMemberInfo_Click(object sender, EventArgs e)
        {
            ///备份会员基本信息
            //BackupMemberInfo();

            if (ddlfs.SelectedValue == "Date")
            {
                ///按照日期备份会员明细表

                ///验证日期
                DateTime beginDate;
                DateTime endDate;

                if (txtBegin.Text.Trim() == "" || txtEnd.Text.Trim() == "")
                {
                    msg = "<script language='javascript'>alert('" + GetTran("004129", "对不起，开始时间和截止时间都不能为空") + "！');</script>";
                    return;
                }

                else
                {
                    beginDate = Convert.ToDateTime(txtBegin.Text.Trim());
                    endDate = Convert.ToDateTime(txtEnd.Text.Trim() + " 23:59");
                    if (beginDate > endDate)
                    {
                        msg = "<script language='javascript'>alert('" + GetTran("004127", "对不起，开始时间必须小于截止时间") + "！');</script>";
                        return;
                    }

                    else
                    {
                        ///按照日期备份会员明细表
                         BackupMemberInfo(beginDate, endDate);
                        msg = "<script language='javascript'>alert('" + GetTran("004137", "按照日期备份会员明细表成功") + "！');</script>";
                    }
                }
            }

            else if (ddlfs.SelectedValue == "ExpectNum")
            {
                ///按照期数备份
                if (ddlqishu.SelectedValue == "")
                {
                    msg = "<script language='javascript'>alert('" + GetTran("004123", "没有数据可备份") + "！')</script>";
                    return;
                }

                BackupMemberInfo(Convert.ToInt32(ddlqishu.SelectedItem.Value));

                msg = "<script language='javascript'>alert('" + GetTran("004135", "按照期数备份会员明细成功") + "！')</script>";
            }
            else
            {
                msg = "<script language='javascript'>alert('" + GetTran("004074", "请选择备份方式") + "！')</script>";
                return;
            }	
        }
        
        /// <summary>
        /// 备份会员明细信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBackupMemberDetails_Click(object sender, EventArgs e)
        {
            if (ddlMemberDetails.SelectedValue == "Date")
            {
                ///按照日期备份会员明细表

                ///验证日期
                DateTime beginDate;
                DateTime endDate;               
               
                if(txtMemberBeginTime.Text.Trim()=="" || txtMemberEndTime.Text.Trim()=="")
                {
                    msg = "<script language='javascript'>alert('" + GetTran("004129", "对不起，开始时间和截止时间都不能为空") + "！');</script>";
                    return;
                }

                else
                {
                    beginDate = Convert.ToDateTime(txtMemberBeginTime.Text.Trim());
                    endDate = Convert.ToDateTime(txtMemberEndTime.Text.Trim() + " 23:59");
                    if(beginDate>endDate)
                    {
                        msg = "<script language='javascript'>alert('" + GetTran("004127", "对不起，开始时间必须小于截止时间") + "！');</script>";
                        return;
                    }

                    else
                    {
                        ///按照日期备份会员明细表
                        BackupMemberDetailsByDate(beginDate, endDate);
                        msg = "<script language='javascript'>alert('" + GetTran("004137", "按照日期备份会员明细表成功") + "！');</script>";
                    }
                }                
            }

            else if (ddlMemberDetails.SelectedValue == "ExpectNum")
            {
                ///按照期数备份
                if (ddlExpectNum.SelectedValue == "")
                {
                    msg = "<script language='javascript'>alert('" + GetTran("004123", "没有数据可备份") + "！')</script>";
                    return;
                }

                BackupMemberDetailsByExpectNum(Convert.ToInt32(ddlExpectNum.SelectedItem.Value));

                msg = "<script language='javascript'>alert('" + GetTran("004135", "按照期数备份会员明细成功") + "！')</script>";
            }
            else
            {
                msg = "<script language='javascript'>alert('" + GetTran("004074", "请选择备份方式") + "！')</script>";
                return;
            }	
        }

        /// <summary>
        /// 会员明细DropDownList事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlMemberDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMemberDetails.SelectedValue == "Date")
            {
                trDate.Visible = true;
                trExpectNum.Visible = false;
            }

            else if (ddlMemberDetails.SelectedValue == "ExpectNum")
            {
                ///重新绑定期数列表
                BindExpectNumList();
                trDate.Visible = false;
                trExpectNum.Visible = true;
            }
            else
            {
                trDate.Visible = false;
                trExpectNum.Visible = false;
            }
        }
        
    /// <summary>
    /// 备份店铺订货产品明细信息（OrderDetail)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnBackupOrderDetail_Click(object sender, EventArgs e)
    {
        if (ddlOrderDetail.SelectedValue == "Date")
        {
            ///按照日期备份专卖店购货商品明细表

            ///验证日期
            DateTime beginDate;
            DateTime endDate;

            if (txtOrderBeginTime.Text.Trim() == "" || txtOrderEndTime.Text.Trim() == "")
            {
                msg = "<script language='javascript'>alert('" + GetTran("004129", "对不起，开始时间和截止时间都不能为空") + "！')</script>";
            }

            else
            {
                beginDate = Convert.ToDateTime(txtOrderBeginTime.Text.Trim());
                endDate = Convert.ToDateTime(txtOrderEndTime.Text.Trim()+ " 23:59");
                if (beginDate > endDate)
                {
                    msg = "<script language='javascript'>alert('" + GetTran("004127", "对不起，开始时间必须小于截止时间") + "！')</script>";
                }

                else
                {
                    ///按照日期备份
                    BackupOrderDetailsByDate(beginDate, endDate);
                    msg = "<script language='javascript'>alert('" + GetTran("004126", "按照日期备份专卖店购货商品明细表成功") + "！')</script>"; 
                }                
            }
        }

        else if (ddlOrderDetail.SelectedValue == "ExpectNum")
        {
            ///按照期数备份专卖店购货商品明细表
            if (ddlDataBackupExpectNum.SelectedValue == "")
            {
                msg = "<script language='javascript'>alert('" + GetTran("004123", "没有数据可备份") + "！')</script>";
                return;
            }

            BackupOrderDetailByExpectNum(Convert.ToInt32(ddlDataBackupExpectNum.SelectedItem.Value));
            msg = "<script language='javascript'>alert('" + GetTran("004122", "按照期数备份专卖店购货商品明细成功") + "！')</script>";
        }
        else
        {
            msg = "<script language='javascript'>alert('" + GetTran("004074", "请选择备份方式") + "！')</script>";
            return;
        }
    }
     
    /// <summary>
    /// 备份店铺订货产品明细DropDownList事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlOrderDetail_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlOrderDetail.SelectedValue== "Date")
        {
            trDataBackupDate.Visible = true;
            trDataBackupExpectNum.Visible = false;
        }
        else if (ddlOrderDetail.SelectedValue == "ExpectNum")
        {
            ///绑定期数列表
            BindBackupExpectNumList();
            trDataBackupDate.Visible = false;
            trDataBackupExpectNum.Visible = true;
        }

        else
        {
            trDataBackupDate.Visible = false;
            trDataBackupExpectNum.Visible = false;
        }
    }

    /// <summary>
    /// 针对导出Excel重载
    /// </summary>
    /// <param name="control"></param>
    public override void VerifyRenderingInServerForm(Control control)
    {
        //base.VerifyRenderingInServerForm(control);
    }

    /// <summary>
    /// 转到数据库备份
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnToBackupDatabase_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Company/BackupDatabase.aspx");
    }
    protected void ddlfs_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlfs.SelectedValue == "Date")
        {
            tr1.Visible = true;
            tr2.Visible = false;
        }
        else if (ddlfs.SelectedValue == "ExpectNum")
        {
            ///绑定期数列表
            BindBackupExpectddlqishu();
            tr1.Visible = false;
            tr2.Visible = true;
        }

        else
        {
            tr1.Visible = false;
            tr2.Visible = false;
        }
    }
}