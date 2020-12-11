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
using BLL.other.Company;
using BLL.CommonClass;
using System.Data.SqlClient;
using DAL;
using Model;
using Model.Other;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using BLL.other.Member;

public partial class Company_AddProduct : BLL.TranslationBase
{
    CommonDataBLL commonDataBLL = new CommonDataBLL();
    LanguageTransModel languageTrans = new LanguageTransModel();

    ///初始化产品实体
    ProductModel productModel = new ProductModel();

    protected string msg;
    protected string action;
    protected int id;

    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now.ToUniversalTime());
        Permissions.CheckManagePermission(EnumCompanyPermission.StorageProductTreeManager);

        if (!IsPostBack)
        {
            if (Request.QueryString["action"] == null || Request.QueryString["action"] == "")
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("003202", "程序调用错误，请联系管理员!")));
                Response.End();
            }

            this.action = Request.QueryString["action"].Trim();
            //产品ID
            this.id = Convert.ToInt32(Request.QueryString["id"].Trim().Replace("N", ""));
            ViewState["ID"] = this.id;


            //接收的国家编码
            if (Request.QueryString["countryCode"] != null)
            {
                ViewState["CountryCode"] = Request.QueryString["countryCode"].Trim();
                ///通过联合查询获取币种ID
                ViewState["CurrencyID"] = AddNewProductBLL.GetMoreCurrencyIDByCountryCode(ViewState["CountryCode"].ToString());
                ///初始化
                IniteCurrency(ViewState["CountryCode"].ToString());
            }

            else
            {
                //通过产品ID获取币种ID
                ViewState["CurrencyID"] = AddNewProductBLL.GetCurrencyIDByProductID(Convert.ToInt32(id));
                //通过产品ID获取国家ID
                ViewState["CountryCode"] = AddNewProductBLL.GetCountryCodeByProductID(Convert.ToInt32(id));
                IniteCurrency(ViewState["CountryCode"].ToString());
            }

            int AdStyleCount = 0, AdProductCount = 0, EdStCount = 0, DeStCount = 0, EdPrCount = 0, DePrCount = 0;

            //Check Rights
            AdStyleCount = Permissions.GetPermissions(EnumCompanyPermission.StorageProductTreeManageAddStyle);
            AdProductCount = Permissions.GetPermissions(EnumCompanyPermission.StorageProductTreeManagerAddProduct);
            EdStCount = Permissions.GetPermissions(EnumCompanyPermission.StorageProductTreeManagerEditStyle);
            DeStCount = Permissions.GetPermissions(EnumCompanyPermission.StorageProductTreeManagerDeleteStyle);
            EdPrCount = Permissions.GetPermissions(EnumCompanyPermission.StorageProductTreeManagerEditProduct);
            DePrCount = Permissions.GetPermissions(EnumCompanyPermission.StorageProductTreeManagerDeleteProduct);

            //Add Product
            if (this.action == "add")
            {
                imgProduct.Visible = false;
                if (AdProductCount.ToString() != "2108")
                {
                    lblMessage.Text = GetTran("003207", "对不起，你没有添加产品权限!");

                    //Right No Pass
                    AddEditRightNoPass_Visible_Enable();
                    return;
                }
                this.lblMessage.Text = GetTran("003212", "当前") + "：" + GetTran("006851", "添加新品");
                lblname.Text = GetTran("002186", "产品");
                this.pID = this.id;
                this.editingID = 0;
                this.addP.Checked = true;

                //1 stand for edit image,0 stand for add image
                ViewState["imageState"] = 0;

                //Right Pass
                AddEditRightPass_Visible_Enable();
            }

            //Add ProductKind
            else if (action == "addFold")
            {
                if (AdStyleCount.ToString() != "2107")
                {
                    lblMessage.Text = GetTran("003222", "对不起,您没有添加新类权限!");

                    //No Pass
                    AddEditFoldNoPass_Visible_Enable();
                    return;
                }

                lblname.Text = GetTran("003224", "类别");
                this.lblMessage.Text = GetTran("003212", "当前") + "：" + GetTran("003228", "添加新类");
                this.pID = this.id;
                this.editingID = 0;
                this.addClass.Checked = true;
                Panel3.Visible = false;
                //Pass
                AddEditFoldPass_Visible_Enable();

                lblclassName.Visible = true;
                //CombineProduct
                chbcombine.Visible = false;

            }
            else if (this.action == "editFold")
            {
                if (EdStCount.ToString() != "2109")
                {
                    lblMessage.Text = GetTran("003232", "对不起,您没有修改产品类权限!");

                    //No Pass                    
                    AddEditFoldNoPass_Visible_Enable();
                    return;
                }
                lblname.Text = GetTran("003224", "类别");
                this.lblMessage.Text = GetTran("003212", "当前") + "：" + GetTran("003236", "编辑产品类");
                this.getItem(id, true);
                this.editingID = this.id;

                //Pass
                AddEditFoldPass_Visible_Enable();

                //CombineProduct
                chbcombine.Visible = false;
                Panel3.Visible = false;
            }

            //Edit Product
            else if (this.action == "editItem")
            {
                imgProduct.Visible = true;
                if (EdPrCount.ToString() != "2111")
                {
                    lblMessage.Text = GetTran("003239", "对不起,您没有修改产品权限!");

                    //Right No Pass
                    AddEditRightNoPass_Visible_Enable();

                    return;
                }
                lblname.Text = GetTran("002186", "产品");
                this.lblMessage.Text = GetTran("003212", "当前") + "：" + GetTran("003243", "编辑产品");
                this.getItem(id, false);
                this.editingID = this.id;

                //Right Pass
                AddEditRightPass_Visible_Enable();
            }

            else if (this.action == "deleteItem")
            {
                if (DePrCount.ToString() != "2112")
                {
                    lblMessage.Text = GetTran("003247", "对不起,您没有删除产品权限!");

                    //No Pass
                    DeleteRightNoPass_Visible_Enable();

                    return;
                }
                Panel3.Visible = false;
                Panel4.Visible = false;
                //No Pass
                DeleteRightNoPass_Visible_Enable();

                this.deleteItem(id);
            }

            else if (this.action == "deleteFold")
            {
                if (DeStCount.ToString() != "2110")
                {
                    lblMessage.Text = GetTran("003251", "对不起,您没有删除产品类权限!");

                    //No Pass
                    DeleteRightNoPass_Visible_Enable();
                    return;
                }
                Panel3.Visible = false;
                Panel4.Visible = false;

                //No Pass
                DeleteRightNoPass_Visible_Enable();
                this.deleteFold(id);
            }

            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("003202", "程序调用错误,请联系管理员!")));
                Response.End();
            }
        }
        Translations_More();
    }

    /// <summary>
    /// Translation
    /// </summary>
    protected void Translations_More()
    {
        TranControls(ChboxHide, new string[][] { new string[] { "001891", "是否销售" } });
        TranControls(doAddButtton, new string[][] { new string[] { "001124", "保 存" } });
        TranControls(ChboxFirst, new string[][] { new string[] { "007101", "首次报单产品" } });
        TranControls(ChboxAgain, new string[][] { new string[] { "007102", "复消报单产品" } });
        TranControls(chbcombine, new string[][] { new string[] { "004211", "是组合产品" } });
        TranControls(UpPhotos, new string[][] { new string[] { "007125", "浏览..." } });
    }

    private int editingID //保存当前编辑项ID 或 添加项 父ID
    {
        get
        {
            int editingID = Convert.ToInt32(ViewState["editingID"]);
            return editingID == 0 ? 0 : editingID;
        }
        set
        {
            ViewState["editingID"] = value;
        }
    }

    private int pID //保存当前编辑项ID 或 添加项 父ID
    {
        get
        {
            int parentID = Convert.ToInt32(ViewState["pID"]);
            return parentID;
        }
        set
        {
            ViewState["pID"] = value;
        }
    }

    /// <summary>
    /// 初始化国家和汇率
    /// </summary>
    /// <param name="countryID"></param>
    private void IniteCurrency(string countryCode)
    {
        //通过国家编码获取国家名称
        this.lblCountry.Text = AddNewProductBLL.GetCountryNameByCountryCode(countryCode);
        //通过国家编码联合查询获取汇率名称
        Label1.Text = AddNewProductBLL.GetMoreCurrencyNameByCountryCode(countryCode);
        Label2.Text = Label1.Text;
        Label3.Text = Label1.Text;
    }

    /// <summary>
    /// Delete right no pass
    /// </summary>
    private void DeleteRightNoPass_Visible_Enable()
    {
        panel2.Visible = false;
        tr_State.Visible = false;
        tr_Combine.Visible = false;
        tr_Description.Visible = false;
        doAddButtton.Visible = false;
    }

    /// <summary>
    /// Add or eidt right pass
    /// </summary>
    private void AddEditRightPass_Visible_Enable()
    {
        chbcombine.Enabled = true;
        txtProductName.Enabled = true;
        UpPhotos.Enabled = true;
        txtDescription.Enabled = true;
        panel2.Enabled = true;
        doAddButtton.Visible = true;
        Panel3.Enabled = false;
    }

    /// <summary>
    /// Add or edit right no pass
    /// </summary>
    private void AddEditRightNoPass_Visible_Enable()
    {
        txtProductName.Enabled = false;
        chbcombine.Enabled = false;
        UpPhotos.Enabled = false;
        txtDescription.Enabled = false;
        panel2.Enabled = false;
        doAddButtton.Visible = false;
        Panel3.Enabled = true;
    }

    /// <summary>
    /// Add or edit fold Pass
    /// </summary>
    private void AddEditFoldPass_Visible_Enable()
    {
        txtProductName.Enabled = true;
        txtDescription.Enabled = true;
        panel2.Visible = false;
        doAddButtton.Visible = true;
    }

    /// <summary>
    ///  Add or edit fold no Pass
    /// </summary>
    private void AddEditFoldNoPass_Visible_Enable()
    {
        txtProductName.Enabled = false;
        txtDescription.Enabled = false;
        panel2.Visible = false;
        doAddButtton.Visible = false;
    }

    /// <summary>
    /// 删除指定产品
    /// </summary>
    /// <param name="id">产品ID</param>
    private void deleteItem(int id)
    {
        ///检查某产品是否放生了业务
        if (AddNewProductBLL.CheckProductWheatherHasOperation(id))
        {
            lblMessage.Text = GetTran("003252", "对不起,该产品已有业务发生，因此不能删除!");
            return;
        }

        using (SqlConnection conn = new SqlConnection(DBHelper.connString))
        {
            conn.Open();
            using (SqlTransaction tran = conn.BeginTransaction())
            {
                try
                {
                    // before delete                        
                    ChangeLogs cl = new ChangeLogs("Product", "ltrim(rtrim(ProductID))");
                    cl.AddRecordtran(tran, Convert.ToString(id));
                    //删除指定产品
                    AddNewProductBLL.DelProductByID(tran, id);

                    cl.DeletedIntoLogstran(tran, ChangeCategory.company7, Session["Company"].ToString(), ENUM_USERTYPE.objecttype1);
                    //提交事务
                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    lblMessage.Text = GetTran("003254", "产品删除失败，请联系管理员!");
                    return;
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }

        lblMessage.Text = GetTran("003255", "产品删除成功");
    }

    /// <summary>
    /// 删除产品类
    /// </summary>
    /// <param name="id">产品ID</param>
    private void deleteFold(int id)
    {
        string productList = string.Empty;
        //检查某产品类是否发生了业务
        bool delFlag = AddNewProductBLL.CheckProductKindWheatherHasOperation(id, out productList);

        if (delFlag)
        {
            this.lblMessage.Text = GetTran("003256", "该产品类已有业务发生，因此不能删除！");
            return;
        }

        using (SqlConnection conn = new SqlConnection(DBHelper.connString))
        {
            conn.Open();
            using (SqlTransaction tran = conn.BeginTransaction())
            {
                try
                {
                    // before delete
                    ChangeLogs cl = new ChangeLogs("Product", "ltrim(rtrim(ProductID))");
                    cl.AddRecordtran(tran, id.ToString());

                    ///删除产品类以及该产品类所属的产品
                    AddNewProductBLL.DelProductKindByID(tran, productList);

                    cl.DeletedIntoLogstran(tran, ChangeCategory.company7, Session["Company"].ToString(), ENUM_USERTYPE.objecttype1);

                    ///提交事务
                    tran.Commit();

                }
                catch
                {
                    tran.Rollback();
                    lblMessage.Text = GetTran("003258", "产品类删除失败,请联系管理员！");
                    return;
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }

        this.lblMessage.Text = GetTran("003260", "产品类删除成功!");
    }

    /// <summary>
    /// 编辑时获取原来数据
    /// </summary>
    /// <param name="itemID">产品ID</param>
    /// <param name="isFold">是否是产品类</param>
    private void getItem(int itemID, bool isFold)
    {
        this.lblMessage.Text = GetTran("004069", "修改产品数据") + "...";
        //根据产品ID获取指定的产品信息
        IList<ProductModel> product = AddNewProductBLL.GetAllProductInfoByID(itemID);

        int isSell = product[0].IsSell;
        //1 stand for no sale
        if (isSell == 1)
        {
            this.ChboxHide.Checked = false;
        }

        //0 stand for on sale
        else
        {
            this.ChboxHide.Checked = true;
        }

        //当编辑为“类”时，隐藏掉不需要修改的字段，只留下“名称”和“说明”输入框
        if (isFold)
        {
            this.addClass.Checked = true;
            this.txtProductName.Text = product[0].ProductName;
            this.txtDescription.Text = product[0].Description;

            panel1.Visible = false;
        }

        //如果修改的是“产品”
        else
        {
            panel1.Visible = false;
            this.txtProductName.Text = product[0].ProductName;

            txtProductType.Text = product[0].ProductTypeName;
            uclProductSpec.SelectedValue = product[0].ProductSpecID.ToString();
            uclProductSexType.SelectedValue = product[0].ProductSexTypeID.ToString();

            this.txtCostPrice.Text = product[0].CostPrice.ToString();
            this.txtCommonPrice.Text = product[0].CommonPrice.ToString();
            this.txtPreferentialPrice.Text = product[0].PreferentialPrice.ToString();
            this.txtDescription.Text = product[0].Description;
            this.txtCommonPV.Text = product[0].CommonPV.ToString();
            this.txtPreferentialPV.Text = product[0].PreferentialPV.ToString();
            this.txtProductCode.Text = product[0].ProductCode;

            uclProductColor.SelectedValue = product[0].ProductColorID.ToString();

            txtProductArea.Text = product[0].ProductArea;
            txtWeight.Text = product[0].Weight.ToString();

            uclProductSize.SelectedValue = product[0].ProductSizeID.ToString();
            uclProductBigUnit.SelectedValue = product[0].BigProductUnitID.ToString();

            uclProductSmallUnit.SelectedValue = product[0].SmallProductUnitID.ToString();
            uclProductStatus.SelectedValue = product[0].ProductStatusID.ToString();

            txtAlertnessCount.Text = product[0].AlertnessCount.ToString();
            txtBigSmallMultiple.Text = product[0].BigSmallMultiple.ToString();

            //---------组合产品
            bool b = Convert.ToBoolean(product[0].IsCombineProduct);
            chbcombine.Checked = Convert.ToBoolean(product[0].IsCombineProduct);


            #region  //xyc新增2011-12-6
            txtDetails.Value = product[0].Details_TX;
            int onlyForGroup = product[0].OnlyForGroup_NR;
            if (onlyForGroup == 0)
                this.rbtnN.Checked = true;
            else if (onlyForGroup == 1)
                this.rbtnY.Checked = true;
            this.txt_GroupIDS_AZ_TX.Text = product[0].GroupIDS_AZ_TX == null ? "" : product[0].GroupIDS_AZ_TX.ToString();
            this.txt_GroupIDS_TJ_TX.Text = product[0].GroupIDS_TJ_TX == null ? "" : product[0].GroupIDS_TJ_TX.ToString();
            #endregion


            if (product[0].ProductImage.Length > 1)
            {
                imgProduct.ImageUrl = FormatURL(product[0].ProductID);
            }
            else
            {
                imgProduct.ImageUrl = @"~/images/pht.GIF";
            }
            this.lblMessage.Text = GetTran("004071", "编辑产品信息");
            //1 stand for edit image,0 stand for add image
            ViewState["imageState"] = 1;
            ViewState["image"] = product[0].ProductImage;
            if (product[0].Yongtu == 0)
            {
                this.ChboxFirst.Checked = true;
                this.ChboxAgain.Checked = true;
            }
            else if (product[0].Yongtu == 1)
            {
                this.ChboxFirst.Checked = true;
                this.ChboxAgain.Checked = false;
            }
            else if (product[0].Yongtu == 2)
            {
                this.ChboxAgain.Checked = true;
                this.ChboxFirst.Checked = false;
            }
        }
    }

    /// <summary>
    /// 得到图片路径
    /// </summary>
    /// <param name="strArgument"></param>
    /// <returns></returns>
    protected string FormatURL(object strArgument)
    {
        string result = "~/ReadImage.aspx?ProductID=" + strArgument.ToString();
        if (result == "" || result == null)
        {
            result = "";
        }
        return result;
    }

    protected void doAddButtton_Click(object sender, EventArgs e)
    {
        //图片大小
        int upPhotoLength = 0;
        byte[] ImageContent = new Byte[upPhotoLength];
        int intStatus = 0;
        //获取上传的文件名
        string fileName = this.UpPhotos.FileName;
        //获取物理路径
        string strImageType = string.Empty;
        Stream PhotoStream = null;

        //当包含文件时(即上传有文件)
        if (UpPhotos.HasFile)
        {
            //为0表示被上传的图片的类型不符合要求，1表示符合要求
            int ImageType = 0;
            HttpPostedFile upPhoto = UpPhotos.PostedFile;
            upPhotoLength = upPhoto.ContentLength;
            if (upPhotoLength > 1024000)
            {
                this.lblMessage.Text = GetTran("004073", "产品图片的大小请限制在300K之内") + "...";
                return;
            }
            //获取上传文件的扩展名
            strImageType = Path.GetExtension(fileName).ToLower();
            String[] Extensions = { ".gif", ".png", ".jpeg", ".jpg", ".bmp", ".pdf" };
            for (int i = 0; i < Extensions.Length; i++)
            {
                if (strImageType == Extensions[i])
                {
                    //图片类型
                    strImageType = Extensions[i];
                    ImageType = 1;
                }
            }

            if (ImageType == 1)
            {
                try
                {
                    PhotoStream = upPhoto.InputStream;
                    ImageContent = new Byte[upPhotoLength];
                    intStatus = PhotoStream.Read(ImageContent, 0, upPhotoLength);
                }
                catch
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("004076", "上传图片失败，请联系管理员!")));
                    return;
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("004079", "请检查图片类型!")));
                return;
            }
        }

        string productType = uclProductType.SelectedValue;
        string typeName = txtProductType.Text.Trim();

        //添加类
        bool isFold = this.addClass.Checked;

        //大单位
        int bignumber = 0;

        ///长、宽、高
        decimal Length = 0, Width = 0, High = 0;

        ///重量
        decimal Weight = 0;

        ///验证预警数量
        int alertCount = 0;

        ///价格和积分
        decimal costPrice = 0, commonPrice = 0, commonPV = 0, preferentialPrice = 0, preferentialPV = 0;


        ///验证产品信息
        if (!isFold)
        {
            //判断产品名称是否为空
            if (txtProductName.Text.Trim() == "")
            {
                this.lblMessage.Text = GetTran("004082", "名称不能为空！");
                return;
            }

            ///产品编码
            if (this.txtProductCode.Text.Trim() == "")
            {
                this.lblMessage.Text = GetTran("004085", "产品编码不能为空！");
                return;
            }

            ///判断产品编码是否重复
            else
            {
                ///添加产品
                if (editingID == 0)
                {
                    //获取编码的数目
                    int proCodeCount = AddNewProductBLL.CheckProductCodeIsExist(this.txtProductCode.Text.Trim());
                    if (proCodeCount > 0)
                    {
                        lblMessage.Text = GetTran("004088", "对不起，编码重复!");
                        return;
                    }
                }

                ///修改产品
                else
                {
                    ///获取行数，判断编码是否存在
                    int proCodeCount = AddNewProductBLL.CheckProductCodeIsExistByID(this.txtProductCode.Text.Trim(), Convert.ToInt32(this.editingID));
                    if (proCodeCount > 0)
                    {
                        lblMessage.Text = GetTran("004088", "对不起，编码重复!");
                        return;
                    }
                }

            }

            ///验证产品型号
            if (this.txtProductType.Text.Trim() == "")
            {
                this.lblMessage.Text = GetTran("004091", "产品型号不能为空！");
                return;
            }

            if (txtCostPrice.Text.Trim() == "")
            {
                costPrice = 0;
            }

            if (txtCommonPrice.Text.Trim() == "")
            {
                commonPrice = 0;
            }

            if (txtPreferentialPrice.Text.Trim() == "")
            {
                preferentialPrice = 0;
            }

            if (txtCommonPV.Text == "")
            {
                commonPV = 0;
            }

            if (txtPreferentialPV.Text == "")
            {
                preferentialPV = 0;
            }

            if (txtCostPrice.Text.Trim() != "" || txtCommonPrice.Text.Trim() != "" || txtCommonPV.Text.Trim() != "" || txtPreferentialPV.Text.Trim() != "" || txtPreferentialPrice.Text.Trim() != "")
            {
                try
                {
                    costPrice = Convert.ToDecimal(this.txtCostPrice.Text.Trim());
                    commonPrice = Convert.ToDecimal(this.txtCommonPrice.Text.Trim());
                    commonPV = Convert.ToDecimal(this.txtCommonPV.Text.Trim());
                    preferentialPrice = Convert.ToDecimal(this.txtPreferentialPrice.Text.Trim());
                    preferentialPV = Convert.ToDecimal(this.txtPreferentialPV.Text.Trim());
                }
                catch
                {
                    this.lblMessage.Text = GetTran("004093", "请正确输入价格,积分!");
                    return;
                }

                if (costPrice < 0 || commonPrice < 0 || commonPV < 0 || preferentialPrice < 0 || preferentialPV < 0)
                {
                    this.lblMessage.Text = GetTran("004094", "请注意，价格,积分不能为负数!");
                    return;
                }
            }

            ///验证大单位
            if (uclProductBigUnit.SelectedItemText == "")
            {
                this.lblMessage.Text = GetTran("004095", "大单位名称不能为空！");
                return;
            }

            ///验证小单位
            if (uclProductSmallUnit.SelectedItemText == "")
            {
                this.lblMessage.Text = GetTran("004096", "小单位名称不能为空！");
                return;
            }

            ///大小单位比例
            if (txtBigSmallMultiple.Text.Trim() == "")
            {
                this.lblMessage.Text = GetTran("004098", "大小单位比例不能为空！");
                return;
            }

            else
            {
                try
                {
                    bignumber = Convert.ToInt32(txtBigSmallMultiple.Text);
                }
                catch
                {
                    lblMessage.Text = GetTran("004100", "对不起，大小单位转换必须是整数！");
                    return;
                }

                if (bignumber <= 0)
                {
                    lblMessage.Text = GetTran("004101", "对不起，大小单位转换必须是大于或等于1的整数！");
                    return;
                }
            }


            Length = 0;
            Width = 0;
            High = 0;

            ///验证重量
            if (txtWeight.Text.Trim() == "")
            {
                this.lblMessage.Text = GetTran("004104", "对不起，重量不能为空！");
                return;
            }

            else
            {
                try
                {
                    Weight = Convert.ToDecimal(txtWeight.Text.Trim());
                }

                catch
                {
                    this.lblMessage.Text = GetTran("004106", "请正确输入重量！");
                    return;
                }

                if (Weight < 0)
                {
                    this.lblMessage.Text = GetTran("004107", "对不起，重量必须大于或等于0！");
                    return;
                }
            }

            ///验证预警数量
            if (txtAlertnessCount.Text.Trim() != "")
            {
                try
                {
                    alertCount = Convert.ToInt32(txtAlertnessCount.Text);
                }
                catch
                {
                    this.lblMessage.Text = GetTran("004108", "对不起，预警数量必须是整数！");
                    return;
                }

                if (alertCount < 0)
                {
                    this.lblMessage.Text = GetTran("004109", "对不起，预警数量必须是大于或等于0的整数！");
                    return;
                }
            }
        }

        ///产品说明
        if (txtDescription.Text.Length > 200)
        {
            this.lblMessage.Text = GetTran("004110", "产品说明不能超过两百字！");
            return;
        }

        string productName = this.txtProductName.Text.Trim();

        ///判断产品类名称是否为空
        if (isFold && productName == "")
        {
            this.lblMessage.Text = GetTran("004082", "名称不能为空！");
            return;
        }

        ///为了和后面的说明区别，故命名为productDescription(2009-10-22)
        string productDescription = ValidData.InputText(this.txtDescription.Text.Trim());
        string productSpec = uclProductSpec.SelectedValue;
        string productColor = uclProductColor.SelectedValue;
        string productStatus = uclProductStatus.SelectedValue;
        string productSexType = uclProductSexType.SelectedValue;
        string productArea = ValidData.InputText(txtProductArea.Text.Trim());
        string productSize = uclProductSize.SelectedValue;
        string productBigUnit = uclProductBigUnit.SelectedValue;
        string productSmallUnit = uclProductSmallUnit.SelectedValue;

        string strMessage;

        //On sale
        int isSell = 0;


        if (this.ChboxHide.Checked == true)
        {
            isSell = 0;
        }

        //No sale
        if (this.ChboxHide.Checked == false)
        {
            isSell = 1;
        }

        #region 产品新增部分[详细、允许销售的团队人数] xyc
        int detailsMaxLenth = 200000;
        string details_TX = Request.Form["txtDetails"] == null ? "" : Request.Form["txtDetails"].ToString();
        details_TX = ValidData.RemoveScript(details_TX);
        //if (details_TX.Length > detailsMaxLenth)
        //{
        //    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert("详细内容过多，请适当减少产品的详细描述内容！"));
        //    return;
        //}
        productModel.Details_TX = details_TX;
        int onlyForGroup = 0;
        if (this.rbtnN.Checked == true)
            onlyForGroup = 0;
        else if (this.rbtnY.Checked == true)
            onlyForGroup = 1;
        productModel.OnlyForGroup_NR = onlyForGroup;
        Regex rx = new Regex(@"[；|;]", RegexOptions.IgnoreCase);
        this.txt_GroupIDS_AZ_TX.Text = rx.Replace(this.txt_GroupIDS_AZ_TX.Text, ";");
        this.txt_GroupIDS_TJ_TX.Text = rx.Replace(this.txt_GroupIDS_TJ_TX.Text, ";");
        string GroupIDS_AZ_TX = this.txt_GroupIDS_AZ_TX.Text;
        string GroupIDS_TJ_TX = this.txt_GroupIDS_TJ_TX.Text;
        Regex rx0 = new Regex(@"\w|;]+", RegexOptions.IgnoreCase);
        if (!string.IsNullOrEmpty(GroupIDS_AZ_TX) && !rx0.IsMatch(GroupIDS_AZ_TX))
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert("格式错误，多安置团队顶点编号请用分号隔开！"));
            return;

        }
        if (!string.IsNullOrEmpty(GroupIDS_TJ_TX) && !rx0.IsMatch(GroupIDS_TJ_TX))
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert("多推荐团队顶点编号请用分号隔开！"));
            return;

        }
        if (onlyForGroup == 1)
        {
            if (string.IsNullOrEmpty(GroupIDS_AZ_TX) && string.IsNullOrEmpty(GroupIDS_TJ_TX))
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert("请填写允许销售的团队顶点编号！"));
                return;
            }
            else
            {
                #region 对会员编号的真实性进行验证
                //验证编号是否存在
                bool bhValidate = true;
                string notExistID = string.Empty;
                string[] bhsAZ = rx.Split(GroupIDS_AZ_TX);
                string[] bhsTJ = rx.Split(GroupIDS_TJ_TX);
                List<string> group_ids = new List<string>();
                foreach (string s in bhsAZ)
                {
                    if (!string.IsNullOrEmpty(s))
                        group_ids.Add(s);
                }
                foreach (string s in bhsTJ)
                {
                    if (!group_ids.Contains(s))
                    {
                        if (!string.IsNullOrEmpty(s))
                            group_ids.Add(s);
                    }
                }

                MemberInfoModel member = null;
                foreach (string s in group_ids)
                {
                    member = MemberInfoModifyBll.getMemberInfo(s);
                    if (member == null)
                    {//不存在
                        bhValidate = false;
                        notExistID += s + ",";
                    }
                }
                if (notExistID != string.Empty)
                {
                    notExistID = notExistID.Substring(0, notExistID.Length - 1);
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('会员[" + notExistID + "]不存在！');</script>", false);
                    return;
                }
                #endregion
            }
        }
        productModel.GroupIDS_AZ_TX = GroupIDS_AZ_TX;
        productModel.GroupIDS_TJ_TX = GroupIDS_TJ_TX;
        #endregion

        productModel.IsFold = Convert.ToInt32(isFold);
        productModel.ProductName = productName;
        if (!isFold)
        {
            productModel.ProductTypeID = Convert.ToInt32(productType);

            productModel.ProductTypeName = typeName;
            productModel.ProductSpecID = Convert.ToInt32(productSpec);
            productModel.ProductColorID = Convert.ToInt32(productColor);
            productModel.ProductSizeID = Convert.ToInt32(productSize);
            productModel.ProductSexTypeID = Convert.ToInt32(productSexType);
            productModel.ProductStatusID = Convert.ToInt32(productStatus);
            productModel.BigProductUnitID = Convert.ToInt32(productBigUnit);
            productModel.SmallProductUnitID = Convert.ToInt32(productSmallUnit);
            productModel.BigSmallMultiple = bignumber;
            productModel.ProductArea = productArea;
            productModel.CostPrice = costPrice;
            productModel.CommonPrice = commonPrice;
            productModel.CommonPV = commonPV;
            productModel.PreferentialPrice = preferentialPrice;
            productModel.PreferentialPV = preferentialPV;
            productModel.AlertnessCount = alertCount;
            productModel.Description = productDescription;
            productModel.Weight = Weight;
            if (ImageContent.Length > 0)
            {
                productModel.ProductImage = ImageContent;
            }

            else
            {
                int imageSate = Convert.ToInt32(ViewState["imageState"]);
                if (imageSate == 0)
                {
                    productModel.ProductImage = ImageContent;
                }

                if (imageSate == 1)
                {
                    productModel.ProductImage = (byte[])ViewState["image"];
                }
            }
            productModel.ImageType = strImageType;
            productModel.CountryCode = ViewState["CountryCode"].ToString();
            productModel.Currency = Convert.ToInt32(ViewState["CurrencyID"]);
            productModel.IsCombineProduct = (byte)(chbcombine.Checked ? 1 : 0);
            productModel.OperateIP = CommonDataBLL.OperateIP;
            productModel.OperateNum = CommonDataBLL.OperateBh;
            productModel.ProductCode = this.txtProductCode.Text.Trim();
            productModel.IsSell = isSell;
            productModel.Length = Length;
            productModel.Width = Width;
            productModel.High = High;
            productModel.Details_TX = Request.Form["txtDetails"].ToString();
            productModel.GroupIDS_AZ_TX = txt_GroupIDS_AZ_TX.Text;
            productModel.GroupIDS_TJ_TX = txt_GroupIDS_TJ_TX.Text;
            productModel.OnlyForGroup_NR = rbtnN.Checked == true ? 0 : 1;
            if (this.ChboxFirst.Checked && this.ChboxAgain.Checked)
            {
                productModel.Yongtu = 0;
            }
            else
            {
                if (this.ChboxFirst.Checked)
                    productModel.Yongtu = 1;
                if (this.ChboxAgain.Checked)
                    productModel.Yongtu = 2;
            }
        }
        else
        {
            productModel.ProductTypeID = 0;
            productModel.ProductTypeName = "";
            productModel.ProductSpecID = 0;
            productModel.ProductColorID = 0;
            productModel.ProductSexTypeID = 0;
            productModel.ProductStatusID = 0;
            productModel.BigProductUnitID = 0;
            productModel.SmallProductUnitID = 0;
            productModel.BigSmallMultiple = 0;
            productModel.ProductArea = "";
            productModel.CostPrice = costPrice;
            productModel.CommonPrice = commonPrice;
            productModel.CommonPV = commonPV;
            productModel.PreferentialPrice = preferentialPrice;
            productModel.PreferentialPV = preferentialPV;
            productModel.AlertnessCount = alertCount;
            productModel.Description = productDescription;
            productModel.ProductImage = ImageContent;
            productModel.ImageType = strImageType;
            productModel.Weight = 0;
            productModel.CountryCode = ViewState["CountryCode"].ToString();
            productModel.Currency = Convert.ToInt32(ViewState["CurrencyID"]);
            productModel.OperateIP = CommonDataBLL.OperateIP;
            productModel.OperateNum = CommonDataBLL.OperateBh;
            productModel.ProductCode = "";
            productModel.IsSell = isSell;
        }

        if (editingID == 0) //添加数据
        {
            bool isEsist = AddNewProductBLL.ProductNameIsExist(productName);
            ///产品
            if (!isFold)
            {
                if (isEsist)
                {
                    this.lblMessage.Text = GetTran("004113", "此产品名称已经存在!");
                    return;
                }
            }

            ///产品类
            else
            {

                if (isEsist)
                {
                    this.lblMessage.Text = GetTran("004116", "此产品类名已经存在!");
                    return;
                }
            }

            productModel.PID = Convert.ToInt32(this.pID);

            strMessage = GetTran("000492", "添加");

            string oldID = "";
            string languageName = "";
            string languageID = "";

            using (SqlConnection conn = new SqlConnection(DBHelper.connString))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        //增加产品                  
                        AddNewProductBLL.AddProduct(tran, productModel);
                        //---多语言产品操作

                        ///获取新产品信息
                        //DataTable dtpro = AddNewProductBLL.GetNewProductInfo();

                        //if(dtpro.Rows.Count>0)
                        //{
                        //    oldID=dtpro.Rows[0]["productid"].ToString();
                        //    languageName=dtpro.Rows[0]["productname"].ToString();

                        //    string description=dtpro.Rows[0]["Description"].ToString().Trim();	

                        //    //获取语言种类(ID>1)
                        //    DataTable dtlang = AddNewProductBLL.GetLanguageIDByID();								

                        //    for(int i=0;i<dtlang.Rows.Count;i++)
                        //    {
                        //        languageID=dtlang.Rows[i][0].ToString();

                        //        //获取----获取(产品目录)是否插入
                        //        int count = AddNewProductBLL.GetLanguageTransCountByID(Convert.ToInt32(languageID));

                        //        if(count==0)
                        //        {
                        //            ///向产品名称等翻译表插入相关记录                                
                        //            languageTrans.TableName="Product";
                        //            languageTrans.OldID=1;
                        //            languageTrans.ColumnsName = productName;
                        //            languageTrans.LanguageName = GetTran("004117","产品目录");
                        //            languageTrans.LanguageID =Convert.ToInt32(languageID);
                        //            AddNewProductBLL.AddLanguageTrans(tran, languageTrans);
                        //        }

                        //        if(description!="")
                        //        {
                        //            ///	向产品名称等翻译表插入相关记录						                                   
                        //            languageTrans.TableName = "Product";
                        //            languageTrans.OldID =Convert.ToInt32(oldID);
                        //            languageTrans.ColumnsName ="Description"; 
                        //            languageTrans.LanguageName=description;
                        //            languageTrans.LanguageID=Convert.ToInt32(languageID);
                        //            AddNewProductBLL.AddLanguageTrans(tran, languageTrans);
                        //        }

                        //        ///向产品名称等翻译表插入相关记录                                         
                        //        languageTrans.TableName = "Product";
                        //        languageTrans.OldID =Convert.ToInt32(oldID);
                        //        languageTrans.ColumnsName = productName;
                        //        languageTrans.LanguageName= languageName;
                        //        languageTrans.LanguageID =Convert.ToInt32(languageID);
                        //        AddNewProductBLL.AddLanguageTrans(tran, languageTrans);
                        //    }
                        //}

                        tran.Commit();
                    }

                    catch (System.Exception ee)
                    {
                        tran.Rollback();
                        System.Diagnostics.Trace.WriteLine(ee.Message);
                        throw;
                    }
                }
            }
        }
        else //修改数据
        {
            strMessage = GetTran("000259", "修改");

            // before delete
            ChangeLogs cl = new ChangeLogs("Product", "ProductID");
            cl.AddRecord(this.editingID);

            //--多语言产品操作(只更新产品说明)

            //获取产品说明
            //string Description = AddNewProductBLL.GetDescriptionByID(Convert.ToInt32(this.editingID)).Trim();                				

            //string langID="";

            //if(Description=="" && productDescription.Trim()!="")
            //{
            //    //查找ID>1的所有ID
            //    DataTable dtlang = AddNewProductBLL.GetLanguageIDByID();

            //    for(int i=0;i<dtlang.Rows.Count;i++)
            //    {
            //        langID=dtlang.Rows[i][0].ToString();

            //        ///插入相关记录                        
            //        languageTrans.TableName = "Product";
            //        languageTrans.OldID = Convert.ToInt32(this.editingID);
            //        languageTrans.ColumnsName = Description;
            //        languageTrans.LanguageName = productDescription;
            //        languageTrans.LanguageID =Convert.ToInt32(langID);
            //        AddNewProductBLL.AddLanguageTrans(languageTrans);
            //    }
            //}

            ///注意传参产品ID
            productModel.ProductID = this.editingID;
            int updCount = AddNewProductBLL.UpdProduct(productModel);


            if (updCount > 0)
            {
                cl.AddRecord(this.editingID);
                cl.ModifiedIntoLogs(ChangeCategory.company7, Session["Company"].ToString(), ENUM_USERTYPE.objecttype1);

                this.lblMessage.Text = strMessage + GetTran("004124", "成功！");
            }
            else
            {
                this.lblMessage.Text = strMessage + GetTran("004128", "失败,请联系管理员!");
            }
        }

        if (this.editingID == 0)
        {
            this.lblMessage.Text += GetTran("004132", "您可以继续添加此类下的产品");
        }

        this.lblMessage.Text += "(" + GetTran("004140", "按 关闭 后可查看新数据") + ")";

        Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("001401", "操作成功!") + "');reloadopener();</script>");
        SetValue();
    }

    /// <summary>
    /// 清空文本框中的值
    /// </summary>
    private void SetValue()
    {
        txtBigSmallMultiple.Text = "";
        txtProductCode.Text = "";
        txtWeight.Text = "";

        txtAlertnessCount.Text = "";
        txtProductName.Text = "";
        txtProductType.Text = "";
        txtCostPrice.Text = "";
        txtCommonPrice.Text = "";
        txtPreferentialPrice.Text = "";
        txtCommonPV.Text = "";
        txtPreferentialPV.Text = "";
        txtProductArea.Text = "";
        txtDescription.Text = "";
        uclProductBigUnit.SelectedValue = "0";
        uclProductColor.SelectedValue = "0";
        uclProductSexType.SelectedValue = "0";
        uclProductSize.SelectedValue = "0";
        uclProductSmallUnit.SelectedValue = "0";
        uclProductSpec.SelectedValue = "0";
        uclProductStatus.SelectedValue = "0";
        uclProductType.SelectedValue = "0";
    }
}