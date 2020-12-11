using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

///Add Namespace
using Model;
using System.Data;
using System.Data.SqlClient;

/*
 *创建者：  汪  华
 *创建时间：2009-08-28
 *文件名：  ProductDAL
 *功能：    对商品进行增删改查操作
 */

namespace DAL
{
    public class ProductDAL
    {
        /// <summary>
        /// 添加产品返回产品ID
        /// </summary>
        /// <param name="productModel"></param>
        /// <returns></returns>
        public static int AddProductItem(SqlTransaction tran, ProductModel productModel)
        {
            string cmd = @"insert into Product([PID],[IsFold],[ProductCode],[ProductName] ,[ProductTypeID],[ProductSpecID]
           ,[ProductColorID],[ProductSizeID],[ProductSexTypeID],[ProductStatusID],[BigProductUnitID],[SmallProductUnitID],[BigSmallMultiple]
           ,[ProductArea],[CostPrice],[CommonPrice],[CommonPV],[PreferentialPrice],[PreferentialPV],[AlertnessCount],[Description]
           ,[Weight],[ProductImage],[ImageType],[IsCombineProduct] ,[ProductType],[ProductTypeName],[Currency],[CountryCode]
           ,[OperateIP],[OperateNum],[IsSell] ,[Length],[Width],[High],[Yongtu],[Details_TX],[OnlyForGroup_NR],[GroupIDS_AZ_TX],[GroupIDS_TJ_TX]) 
            values (@PID,@IsFold,@ProductCode,@ProductName ,@ProductTypeID
           ,@ProductSpecID,@ProductColorID,@ProductSizeID,@ProductSexTypeID,@ProductStatusID,@BigProductUnitID,@SmallProductUnitID,@BigSmallMultiple
           ,@ProductArea,@CostPrice,@CommonPrice,@CommonPV,@PreferentialPrice,@PreferentialPV,@AlertnessCount,@Description
           ,@Weight,@ProductImage,@ImageType,@IsCombineProduct ,@ProductType,@ProductModelName,@Currency,@CountryCode,@OperateIP,@OperateNum,@IsSell 
           ,@Length,@Width,@High,@Yongtu,@Details_TX,@OnlyForGroup_NR,@GroupIDS_AZ_TX,@GroupIDS_TJ_TX)

            select @@identity
            ";

            SqlParameter[] param = new SqlParameter[] 
            {
                new SqlParameter("@PID",productModel.PID),
                new SqlParameter("@IsFold",productModel.IsFold),
                new SqlParameter("@ProductCode",productModel.ProductCode),
                new SqlParameter("@ProductName",productModel.ProductName),
                new SqlParameter("@ProductTypeID",productModel.ProductTypeID),
                new SqlParameter("@ProductSpecID",productModel.ProductSpecID),
                new SqlParameter("@ProductColorID",productModel.ProductColorID),
                new SqlParameter("@ProductSizeID",productModel.ProductSizeID),
                new SqlParameter("@ProductSexTypeID",productModel.ProductSexTypeID),
                new SqlParameter("@ProductStatusID",productModel.ProductStatusID),            
                new SqlParameter("@BigProductUnitID",productModel.BigProductUnitID),
                new SqlParameter("@SmallProductUnitID",productModel.SmallProductUnitID),
                new SqlParameter("@BigSmallMultiple",productModel.BigSmallMultiple),
                new SqlParameter("@ProductArea",productModel.ProductArea),
                new SqlParameter("@CostPrice",productModel.CostPrice),
                new SqlParameter("@CommonPrice",productModel.CommonPrice),
                new SqlParameter("@CommonPV",productModel.CommonPV),
                new SqlParameter("@PreferentialPrice",productModel.PreferentialPrice),
                new SqlParameter("@PreferentialPV",productModel.PreferentialPV),
                new SqlParameter("@AlertnessCount",productModel.AlertnessCount),
                new SqlParameter("@Description",productModel.Description),
                new SqlParameter("@Weight",productModel.Weight),               
                new SqlParameter("@ProductImage",productModel.ProductImage),
                new SqlParameter("@ImageType",productModel.ImageType),
                new SqlParameter("@IsCombineProduct",productModel.IsCombineProduct),
                new SqlParameter("@ProductType",productModel.ProductType),
                new SqlParameter("@ProductModelName",productModel.ProductTypeName),
                new SqlParameter("@Currency",productModel.Currency),               
                new SqlParameter("@CountryCode",productModel.CountryCode),
                new SqlParameter("@OperateIP",productModel.OperateIP),
                new SqlParameter("@OperateNum",productModel.OperateNum),
                new SqlParameter("@IsSell",productModel.IsSell),
                new SqlParameter("@Length",productModel.Length),
                new SqlParameter("@Width",productModel.Width),
                new SqlParameter("@High",productModel.High),
                new SqlParameter("@Yongtu",productModel.Yongtu),
                new SqlParameter("@GroupIDS_TJ_TX",productModel.GroupIDS_TJ_TX),
                new SqlParameter("@GroupIDS_AZ_TX",productModel.GroupIDS_AZ_TX),
                new SqlParameter("@OnlyForGroup_NR",productModel.OnlyForGroup_NR),
                new SqlParameter("@Details_TX",productModel.Details_TX)
            };


            return DBHelper.ExecuteNonQuery(cmd, param, CommandType.Text);
        }

        /// <summary>
        /// 根据产品ID获取产品信息
        /// </summary>
        /// <param name="productid"></param>
        /// <returns></returns>
        public static DataTable SelectProductById(string productid)
        {
            string StrSql = "select ProductID,ProductName,CommonPrice,CommonPV,PreferentialPrice,PreferentialPV,Description,ProductImage,currency.name as currency from Product,currency where Product.currency=currency.id and ProductID=@ProID";

            SqlParameter[] para = 
            { 
              new SqlParameter("@ProID",productid)
            };
            return DAL.DBHelper.ExecuteDataTable(StrSql, para, CommandType.Text);
        }

        /// <summary>
        /// 删除产品
        /// </summary>
        /// <param name="productID">产品ID</param>
        /// <returns>返回删除产品所影响的行数</returns>
        public static int DelProductByID(SqlTransaction tran, int productID)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@ProductID",SqlDbType.Int)
            };
            sparams[0].Value = productID;

            return DBHelper.ExecuteNonQuery("DelProductByID", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 修改产品
        /// </summary>
        /// <param name="productModel"></param>
        /// <returns></returns>
        public static int UpdProductItem(ProductModel productModel)
        {
            string cmd = @"update Product set IsFold=@IsFold,ProductCode=@ProductCode,ProductName=@ProductName ,ProductTypeID=@ProductModelID
           ,ProductSpecID=@ProductSpecID,ProductColorID=@ProductColorID,ProductSizeID=@ProductSizeID,ProductSexTypeID=@ProductSexTypeID
           ,ProductStatusID=@ProductStatusID,BigProductUnitID=@BigProductUnitID,SmallProductUnitID=@SmallProductUnitID,BigSmallMultiple=@BigSmallMultiple
           ,ProductArea=@ProductArea,CostPrice=@CostPrice,CommonPrice=@CommonPrice,CommonPV=@CommonPV,PreferentialPrice=@PreferentialPrice
           ,PreferentialPV=@PreferentialPV,AlertnessCount=@AlertnessCount,Description=@Description,Weight=@Weight,ProductImage=@ProductImage,ImageType=@ImageType,IsCombineProduct=@IsCombineProduct ,ProductType=@ProductType,ProductTypeName=@ProductModelName
           ,Currency=@Currency,CountryCode=@CountryCode,OperateIP=@OperateIP,OperateNum=@OperateNum,IsSell=@IsSell ,Length=@Length,Width=@Width,High=@High ,Yongtu=@Yongtu,Details_TX=@Details_TX,OnlyForGroup_NR=@OnlyForGroup_NR,GroupIDS_AZ_TX=@GroupIDS_AZ_TX,GroupIDS_TJ_TX=@GroupIDS_TJ_TX
            where ProductID=@ProductID";

            SqlParameter[] param = new SqlParameter[] 
            {                
                new SqlParameter("@IsFold",productModel.IsFold),
                new SqlParameter("@ProductCode",productModel.ProductCode),
                new SqlParameter("@ProductName",productModel.ProductName),
                new SqlParameter("@ProductModelID",productModel.ProductTypeID),
                new SqlParameter("@ProductSpecID",productModel.ProductSpecID),
                new SqlParameter("@ProductColorID",productModel.ProductColorID),
                new SqlParameter("@ProductSizeID",productModel.ProductSizeID),
                new SqlParameter("@ProductSexTypeID",productModel.ProductSexTypeID),
                new SqlParameter("@ProductStatusID",productModel.ProductStatusID),                
                new SqlParameter("@BigProductUnitID",productModel.BigProductUnitID),
                new SqlParameter("@SmallProductUnitID",productModel.SmallProductUnitID),
                new SqlParameter("@BigSmallMultiple",productModel.BigSmallMultiple),
                new SqlParameter("@ProductArea",productModel.ProductArea),
                new SqlParameter("@CostPrice",productModel.CostPrice),
                new SqlParameter("@CommonPrice",productModel.CommonPrice),
                new SqlParameter("@CommonPV",productModel.CommonPV),
                new SqlParameter("@PreferentialPrice",productModel.PreferentialPrice),
                new SqlParameter("@PreferentialPV",productModel.PreferentialPV),
                new SqlParameter("@AlertnessCount",productModel.AlertnessCount),
                new SqlParameter("@Description",productModel.Description),
                new SqlParameter("@Weight",productModel.Weight),               
                new SqlParameter("@ProductImage",productModel.ProductImage),
                new SqlParameter("@ImageType",productModel.ImageType),
                new SqlParameter("@IsCombineProduct",productModel.IsCombineProduct),
                new SqlParameter("@ProductType",productModel.ProductType),
                new SqlParameter("@ProductModelName",productModel.ProductTypeName),
                new SqlParameter("@Currency",productModel.Currency),                
                new SqlParameter("@CountryCode",productModel.CountryCode),
                new SqlParameter("@OperateIP",productModel.OperateIP),
                new SqlParameter("@OperateNum",productModel.OperateNum),
                new SqlParameter("@IsSell",productModel.IsSell),
                new SqlParameter("@Length",productModel.Length),
                new SqlParameter("@Width",productModel.Width),
                new SqlParameter("@High",productModel.High),
                new SqlParameter("@ProductID",productModel.ProductID),
                new SqlParameter("@Yongtu",productModel.Yongtu),
                new SqlParameter("@GroupIDS_TJ_TX",productModel.GroupIDS_TJ_TX),
                new SqlParameter("@GroupIDS_AZ_TX",productModel.GroupIDS_AZ_TX),
                new SqlParameter("@OnlyForGroup_NR",productModel.OnlyForGroup_NR),
                new SqlParameter("@Details_TX",productModel.Details_TX)
            };

            return DBHelper.ExecuteNonQuery(cmd, param, CommandType.Text);
        }

        /// <summary>
        /// 查询产品
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public static ProductModel GetProductItem(int productID)
        {
            string cmd = @"select [ProductID],[PID],[IsFold],[ProductCode],[ProductName] ,[ProductModelID],[ProductSpecID]
           ,[ProductColorID],[ProductSizeID],[ProductSexTypeID],[ProductStatusID],[SmallProductUnitID],[BigSmallMultiple]
           ,[ProductArea],[CostPrice],[CommonPrice],[CommonPV],[PreferentialPrice],[PreferentialPV],[AlertnessCount],[Description]
           ,[Weight],[ProductImage],[ImageType],[IsCombineProduct] ,[ProductType],[ProductModelName],[Currency],[CountryCode] ,
            [OperateIP],[OperateNum],[IsSell] ,[Length],[Width],[High],[Yongtu] from Product where ProductID=@ProductID";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@ProductID",productID)
            };

            SqlDataReader dr = DBHelper.ExecuteReader(cmd, param, CommandType.Text);

            dr.Read();

            ProductModel pm = new ProductModel();

            pm.ProductID = Convert.ToInt32(dr["ProductID"]);
            pm.PID = Convert.ToInt32(dr["PID"]);
            pm.IsFold = Convert.ToInt32(dr["IsFold"]);
            pm.ProductCode = dr["ProductCode"].ToString();
            pm.ProductName = dr["ProductName"].ToString();
            pm.ProductTypeID = Convert.ToInt32(dr["ProductTypeID"]);
            pm.ProductSpecID = Convert.ToInt32(dr["ProductSpecID"]);
            pm.ProductColorID = Convert.ToInt32(dr["ProductColorID"]);
            pm.ProductSizeID = Convert.ToInt32(dr["ProductSizeID"]);
            pm.ProductSexTypeID = Convert.ToInt32(dr["ProductSexTypeID"]);
            pm.ProductStatusID = Convert.ToInt32(dr["ProductStatusID"]);
            pm.SmallProductUnitID = Convert.ToInt32(dr["SmallProductUnitID"]);
            pm.BigSmallMultiple = Convert.ToInt32(dr["BigSmallMultiple"]);
            pm.ProductArea = dr["ProductArea"].ToString();
            pm.CostPrice = Convert.ToInt32(dr["CostPrice"]);
            pm.CommonPrice = Convert.ToInt32(dr["CommonPrice"]);
            pm.CommonPV = Convert.ToInt32(dr["CommonPV"]);
            pm.PreferentialPrice = Convert.ToInt32(dr["PreferentialPrice"]);
            pm.PreferentialPV = Convert.ToInt32(dr["PreferentialPV"]);
            pm.AlertnessCount = Convert.ToInt32(dr["AlertnessCount"]);
            pm.Description = dr["Description"].ToString();
            pm.Weight = Convert.ToInt32(dr["Weight"]);
            pm.ProductImage = (byte[])dr["ProductImage"];
            pm.ImageType = dr["ImageType"].ToString();
            pm.IsCombineProduct = Convert.ToInt32(dr["IsCombineProduct"]);
            pm.ProductType = Convert.ToInt32(dr["ProductType"]);
            pm.ProductTypeName = dr["ProductTypeName"].ToString();
            pm.Currency = Convert.ToInt32(dr["Currency"]);
            pm.CountryCode = dr["CountryCode"].ToString();
            pm.OperateIP = dr["OperateIP"].ToString();
            pm.OperateNum = dr["OperateNum"].ToString();
            pm.IsSell = Convert.ToInt32(dr["IsSell"]);
            pm.Length = Convert.ToInt32(dr["Length"]);
            pm.Width = Convert.ToInt32(dr["Width"]);
            pm.High = Convert.ToInt32(dr["High"]);
            pm.Yongtu = Convert.ToInt32(dr["Yongtu"]);
            dr.Close();

            return pm;
        }

        /// <summary>
        /// 添加产品类
        /// </summary>
        /// <param name="productModel"></param>
        /// <returns>返回受影响的行数</returns>
        public static int AddProductKind(ProductModel productModel)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@pID",productModel.PID),
                new SqlParameter("@isFold",productModel.IsFold),
                new SqlParameter("@productName",productModel.ProductName),
                new SqlParameter("@description",productModel.Description),
                new SqlParameter("@countryCode",productModel.CountryCode)
            };

            return DBHelper.ExecuteNonQuery("AddProductKind", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 更新产品类
        /// </summary>
        /// <param name="productModel"></param>
        /// <returns>返回更新产品类所影响的行数</returns>
        public static int UpdProductKind(ProductModel productModel)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@productID",productModel.ProductID),
                new SqlParameter("@productName",productModel.ProductName),
                new SqlParameter("@description",productModel.Description),
                new SqlParameter("@isSell",productModel.IsSell)
            };

            return DBHelper.ExecuteNonQuery("UpdProductKind", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 删除产品类  ---DS2012
        /// </summary>
        /// <param name="tran">要处理的事务</param>
        /// <param name="productListID">产品类及类所属的产品ID</param>
        /// <returns>返回删除产品类所影响的行数</returns>
        public static int DelProductKindByID(SqlTransaction tran, string productListID)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@productListID",SqlDbType.VarChar,4000)
            };
            sparams[0].Value = productListID;

            return DBHelper.ExecuteNonQuery("DelProductKindByID", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 获取产品(产品类)名称
        /// </summary>
        /// <param name="productName">产品名称</param>
        /// <returns>当产品名称有重复时，则返回真，否则返回假</returns>
        public static bool GetProductName(string productName)
        {
            int getRowCount = 0;
            SqlParameter[] sparams = new SqlParameter[] 
            {
                new SqlParameter("@productName",SqlDbType.VarChar,100)
            };
            sparams[0].Value = productName;

            getRowCount = (int)DBHelper.ExecuteScalar("GetProductName", sparams, CommandType.StoredProcedure);
            if (getRowCount > 0)
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        /// <summary>
        /// 通过产品ID获取国家编码
        /// </summary>
        /// <param name="productID">产品ID</param>
        /// <returns>返回国家编码</returns>
        public static string GetCountryCodeByProductID(int productID)
        {
            SqlParameter[] sparams = new SqlParameter[] 
            {
                new SqlParameter("@productID",SqlDbType.Int)
            };

            sparams[0].Value = productID;

            return DBHelper.ExecuteScalar("GetCountryCodeByProductID", sparams, CommandType.StoredProcedure).ToString();
        }

        /// <summary>
        /// 获取产品描述
        /// </summary>
        /// <param name="productID">产品ID</param>
        /// <returns>返回产品说明</returns>
        public static string GetDescriptionByID(int productID)
        {
            SqlParameter[] sparams = new SqlParameter[] 
            {
                new SqlParameter("@productID",SqlDbType.Int)
            };
            sparams[0].Value = productID;

            return Convert.ToString(DBHelper.ExecuteScalar("GetDescriptionByID", sparams, CommandType.StoredProcedure).ToString());
        }

        /// <summary>
        /// 通过产品编码获取行数
        /// </summary>
        /// <param name="productCode">产品编码</param>
        /// <returns>返回所获取的行数</returns>
        public static int GetCountByProductCode(string productCode)
        {
            SqlParameter[] sparams = new SqlParameter[] 
            {
                new SqlParameter("@productCode",SqlDbType.VarChar,100)
            };

            sparams[0].Value = productCode;

            return Convert.ToInt32(DBHelper.ExecuteScalar("GetCountByProductCode", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// 获取新产品信息
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetNewProductInfo()
        {
            return DBHelper.ExecuteDataTable("GetNewProductInfo", CommandType.StoredProcedure);
        }

        /// <summary>
        /// 获取产品树型关系列表  ---DS2012
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetTreeProductList()
        {
            return DBHelper.ExecuteDataTable("GetTreeProductList", CommandType.StoredProcedure);
        }

        /// <summary>
        /// 获取产品ID和产品说明
        /// </summary>
        /// <param name="tran">要处理的事务</param>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetProductIDDescriptionByDescription(SqlTransaction tran)
        {
            return DBHelper.ExecuteDataTable("GetProductIDDescriptionByDescription", CommandType.StoredProcedure);
        }

        /// <summary>
        /// 根据产品ID获取指定的所有产品信息
        /// </summary>
        /// <param name="productID">产品ID</param>
        /// <returns>返回集合</returns>
        public static IList<ProductModel> GetAllProductInfoByID(int productID)
        {
            IList<ProductModel> productList = new List<ProductModel>();
            SqlParameter[] sparams = new SqlParameter[] 
            {
                new SqlParameter("@productID",SqlDbType.Int)
            };
            sparams[0].Value = productID;
            SqlDataReader dr;
            dr = DBHelper.ExecuteReader("GetAllProductInfoByID", sparams, CommandType.StoredProcedure);

            while (dr.Read())
            {
                ProductModel productModel = new ProductModel();
                productModel.ProductID = Convert.ToInt32(dr["ProductID"]);
                productModel.PID = Convert.ToInt32(dr["PID"]);
                productModel.IsFold = Convert.ToInt32(dr["IsFold"]);
                productModel.ProductCode = dr["ProductCode"].ToString();
                productModel.ProductName = dr["ProductName"].ToString();
                productModel.ProductTypeID = Convert.ToInt32(dr["ProductTypeID"]);
                productModel.ProductSpecID = Convert.ToInt32(dr["ProductSpecID"]);
                productModel.ProductColorID = Convert.ToInt32(dr["ProductColorID"]);
                productModel.ProductSizeID = Convert.ToInt32(dr["ProductSizeID"]);
                productModel.ProductSexTypeID = Convert.ToInt32(dr["ProductSexTypeID"]);
                productModel.ProductStatusID = Convert.ToInt32(dr["ProductStatusID"]);
                productModel.BigProductUnitID = Convert.ToInt32(dr["BigProductUnitID"]);
                productModel.SmallProductUnitID = Convert.ToInt32(dr["SmallProductUnitID"]);
                productModel.BigSmallMultiple = Convert.ToInt32(dr["BigSmallMultiple"]);
                productModel.ProductArea = dr["ProductArea"].ToString();
                productModel.CostPrice = Convert.ToDecimal(dr["CostPrice"]);
                productModel.CommonPrice = Convert.ToDecimal(dr["CommonPrice"]);
                productModel.CommonPV = Convert.ToDecimal(dr["CommonPV"]);
                productModel.PreferentialPrice = Convert.ToDecimal(dr["PreferentialPrice"]);
                productModel.PreferentialPV = Convert.ToDecimal(dr["PreferentialPV"]);
                productModel.AlertnessCount = Convert.ToInt32(dr["AlertnessCount"]);
                productModel.Description = dr["Description"].ToString();
                productModel.Weight = Convert.ToDecimal(dr["Weight"]);
                productModel.ProductImage = (byte[])dr["ProductImage"];
                productModel.ImageType = dr["ImageType"].ToString();
                productModel.IsCombineProduct = Convert.ToInt32(dr["IsCombineProduct"]);
                productModel.ProductType = Convert.ToInt32(dr["ProductType"]);
                productModel.ProductTypeName = dr["ProductTypeName"].ToString();
                productModel.Currency = Convert.ToInt32(dr["Currency"]);
                productModel.CountryCode = dr["CountryCode"].ToString();
                productModel.OperateIP = dr["OperateIP"].ToString();
                productModel.OperateNum = dr["OperateNum"].ToString();
                productModel.IsSell = Convert.ToInt32(dr["IsSell"]);
                productModel.Length = Convert.ToDecimal(dr["Length"]);
                productModel.Width = Convert.ToDecimal(dr["Width"]);
                productModel.High = Convert.ToDecimal(dr["High"]);
                productModel.Yongtu = Convert.ToInt32(dr["Yongtu"]);
                productModel.GroupIDS_AZ_TX = dr["GroupIDS_AZ_TX"].ToString();
                productModel.GroupIDS_TJ_TX = dr["GroupIDS_TJ_TX"].ToString();
                productModel.Details_TX = dr["Details_TX"].ToString();
                productList.Add(productModel);
            }
            dr.Close();
            return productList;
        }

        /// <summary>
        /// 根据产品ID获取指定的产品信息
        /// </summary>
        /// <param name="productID">产品ID</param>
        /// <returns>返回集合</returns>
        public static ProductModel GetProductById(int productID)
        {
         
            SqlParameter[] sparams = new SqlParameter[] 
            {
                new SqlParameter("@productID",SqlDbType.Int)
               
            };
         
            sparams[0].Value = productID;
            SqlDataReader dr = DBHelper.ExecuteReader("select * from product where productid=@productID", sparams, CommandType.Text);

            ProductModel productModel = new ProductModel();
            while (dr.Read())
            {
                productModel.ProductID = Convert.ToInt32(dr["ProductID"]);
                productModel.PID = Convert.ToInt32(dr["PID"]);
                productModel.IsFold = Convert.ToInt32(dr["IsFold"]);
                productModel.ProductCode = dr["ProductCode"].ToString();
                productModel.ProductName = dr["ProductName"].ToString();
                productModel.ProductTypeID = Convert.ToInt32(dr["ProductTypeID"]);
                productModel.ProductSpecID = Convert.ToInt32(dr["ProductSpecID"]);
                productModel.ProductColorID = Convert.ToInt32(dr["ProductColorID"]);
                productModel.ProductSizeID = Convert.ToInt32(dr["ProductSizeID"]);
                productModel.ProductSexTypeID = Convert.ToInt32(dr["ProductSexTypeID"]);
                productModel.ProductStatusID = Convert.ToInt32(dr["ProductStatusID"]);
                productModel.BigProductUnitID = Convert.ToInt32(dr["BigProductUnitID"]);
                productModel.SmallProductUnitID = Convert.ToInt32(dr["SmallProductUnitID"]);
                productModel.BigSmallMultiple = Convert.ToInt32(dr["BigSmallMultiple"]);
                productModel.ProductArea = dr["ProductArea"].ToString();
                productModel.CostPrice = Convert.ToDecimal(dr["CostPrice"]);
                productModel.CommonPrice = Convert.ToDecimal(dr["CommonPrice"]);
                productModel.CommonPV = Convert.ToDecimal(dr["CommonPV"]);
                productModel.PreferentialPrice = Convert.ToDecimal(dr["PreferentialPrice"]);
                productModel.PreferentialPV = Convert.ToDecimal(dr["PreferentialPV"]);
                productModel.AlertnessCount = Convert.ToInt32(dr["AlertnessCount"]);
                productModel.Description = dr["Description"].ToString();
                productModel.Weight = Convert.ToDecimal(dr["Weight"]);
                productModel.ProductImage = (byte[])dr["ProductImage"];
                productModel.ImageType = dr["ImageType"].ToString();
                productModel.IsCombineProduct = Convert.ToInt32(dr["IsCombineProduct"]);
                productModel.ProductType = Convert.ToInt32(dr["ProductType"]);
                productModel.ProductTypeName = dr["ProductTypeName"].ToString();
                productModel.Currency = Convert.ToInt32(dr["Currency"]);
                productModel.CountryCode = dr["CountryCode"].ToString();
                productModel.OperateIP = dr["OperateIP"].ToString();
                productModel.OperateNum = dr["OperateNum"].ToString();
                productModel.IsSell = Convert.ToInt32(dr["IsSell"]);
                productModel.Length = Convert.ToDecimal(dr["Length"]);
                productModel.Width = Convert.ToDecimal(dr["Width"]);
                productModel.High = Convert.ToDecimal(dr["High"]);
                productModel.Yongtu = Convert.ToInt32(dr["Yongtu"]);
            }
            dr.Close();

            return productModel;
        }

        /// <summary>
        /// 判断产品编码是否重复
        /// </summary>
        /// <param name="productCode">产品编码</param>
        /// <returns>返回产品编码的数目</returns>
        public static int CheckProductCodeIsExist(string productCode)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@productCode",SqlDbType.VarChar,100)
            };
            sparams[0].Value = productCode;

            return Convert.ToInt32(DBHelper.ExecuteScalar("CheckProductCodeIsExist", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// 通过产品ID和产品名称判断产品编码是否重复
        /// </summary>
        /// <param name="productName">产品名称</param>
        /// <param name="productID">产品ID</param>
        /// <returns>返回产品编码的数目</returns>
        public static int CheckProductCodeIsExistByID(string productName, int productID)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@productCode",SqlDbType.VarChar,100),
                new SqlParameter("@productID",SqlDbType.Int)
            };

            sparams[0].Value = productName;
            sparams[1].Value = productID;

            return Convert.ToInt32(DBHelper.ExecuteScalar("CheckProductCodeIsExistByID", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// 检查某产品是否放生了业务  ---DS2012
        /// </summary>
        /// <param name="productID">产品ID</param>
        /// <returns>当发生了业务，返回为真，否则返回假</returns>
        public static bool CheckProductWheatherHasOperation(int productID)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@productID",SqlDbType.Int)            
            };
            sparams[0].Value = productID;
            SqlDataReader reader = DBHelper.ExecuteReader("CheckProductWheatherHasOperation", sparams, CommandType.StoredProcedure);
            if (reader.Read())
            {
                reader.Close();
                return true;
            }

            else
            {
                reader.Close();
                return false;
            }
        }

        /// <summary>
        /// 检查某产品类是否放生了业务   ---DS2012
        /// </summary>
        /// <param name="productID"></param>
        /// <param name="allproductIdList">所有产品ID列表</param>
        /// <returns>如果该产品类发生了业务，则返回真，否则返回假</returns>
        public static bool CheckProductKindWheatherHasOperation(int productID, out string allproductIdList)
        {
            string productIdList = string.Empty;
            allproductIdList = string.Empty;

            DataTable dtProductTree = new DataTable();
            if (productID != 0)
            {
                dtProductTree = GetTreeProductList();

                GetChildProductIDList(dtProductTree, productID, ref productIdList, ref allproductIdList);

                allproductIdList = allproductIdList.TrimEnd(",".ToCharArray());
                if (productIdList.Length != 0)
                {
                    productIdList = productIdList.TrimEnd(",".ToCharArray());
                }
                else
                    return false;
            }

            string checkSQL = "Select Top 1 ProductID  From  MemberDetails Where ProductID IN (" + productIdList + ")";
            checkSQL += " union All ";
            checkSQL += "Select Top 1 ProductID  From  OrderDetail  Where ProductID IN (" + productIdList + ")";
            checkSQL += " union All ";
            checkSQL += "Select Top 1 productID From InventoryDocDetails  Where productID IN (" + productIdList + ")";
            checkSQL += " union All ";
            checkSQL += "select top 1 SubProductID from ProductCombineDetail where  SubProductID in (" + productIdList + ")";

            SqlDataReader reader = DBHelper.ExecuteReader(checkSQL);

            if (reader.Read())
            {
                reader.Close();
                return true;
            }
            else
            {
                reader.Close();
                return false;
            }
        }

        /// <summary>
        /// 检查产品树型结构的某个记录下的所有节点  ---DS2012
        /// </summary>
        /// <param name="dtProductTree">产品DataTable</param>
        /// <param name="productID">产品ID</param>
        /// <param name="productIdList">只是产品的productID字符串</param>
        /// <param name="allproductIdList">所有的productID字符串</param>	
        private static void GetChildProductIDList(DataTable dtProductTree, int productID, ref string productIdList, ref string allproductIdList)
        {
            DataRow[] productRow;
            productRow = dtProductTree.Select("PID=" + productID);
            allproductIdList += productID + ",";

            for (int i = 0; i < productRow.Length; i++)
            {
                if (productRow[i]["isFold"].ToString() == "0")
                {
                    productIdList += productRow[i]["ProductID"].ToString() + ",";
                    allproductIdList += productRow[i]["ProductID"].ToString() + ",";
                }
                else
                {
                    GetChildProductIDList(dtProductTree, Convert.ToInt32(productRow[i]["ProductID"].ToString()), ref   productIdList, ref allproductIdList);
                }
            }
        }

        /// <summary>
        /// 通过产品ID获取产品名称
        /// </summary>
        /// <param name="productID">产品ID</param>
        /// <returns>返回产品名称</returns>
        public static string GetProductNameByID(int productID)
        {
            SqlParameter[] sparams = new SqlParameter[] 
            {
                new SqlParameter("@productID",SqlDbType.Int)
            };
            sparams[0].Value = productID;

            return Convert.ToString(DBHelper.ExecuteScalar("GetProductNameByID", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// 通过产品ID获取产品名称
        /// </summary>
        /// <param name="productID">产品ID</param>
        /// <returns>返回产品名称</returns>
        public static double GetProductpriceByID(int productID)
        {
            SqlParameter[] sparams = new SqlParameter[] 
            {
                new SqlParameter("@productID",SqlDbType.Int)
            };
            sparams[0].Value = productID;

            return Convert.ToDouble(DBHelper.ExecuteScalar("select PreferentialPrice from product where productid=@productID", sparams, CommandType.Text));
        }

        /// <summary>
        /// 获取产品ID和产品名称
        /// </summary>
        /// <param name="tran">处理的事务</param>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetProductIDNameOrderByProductID(SqlTransaction tran)
        {
            DataTable dt;
            dt = DBHelper.ExecuteDataTable("GetProductIDNameOrderByProductID", CommandType.StoredProcedure);
            return dt;
        }


        /// <summary>
        /// 通过产品编码获取产品信息
        /// </summary>
        /// <param name="productCode">产品编码</param>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetProductInfoByProductCode(string productCode)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@productCode",SqlDbType.VarChar,100)                   
            };
            sparams[0].Value = productCode;

            return DBHelper.ExecuteDataTable("GetProductInfoByProductCode", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 通过联合查询获取产品详细信息
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetMoreProductDetailsInfo()
        {
            return DBHelper.ExecuteDataTable("GetMoreProductDetailsInfo", CommandType.StoredProcedure);
        }

        /// <summary>
        /// Out to excle the data of ProductDetailsInfo 
        /// </summary>
        /// <returns>Return DataTable Object</returns>
        public static DataTable OutToExcel_ProductDetailsInfo()
        {
            return DBHelper.ExecuteDataTable("OutToExcel_ProductDetailsInfo", CommandType.StoredProcedure);
        }

        /// <summary>
        /// 通过产品ID获取价格PV信息
        /// </summary>
        /// <param name="productID">产品ID</param>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetPricePVByID(int productID)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@productID",SqlDbType.Int)
            };

            sparams[0].Value = productID;

            return DBHelper.ExecuteDataTable("GetPricePVByID", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 通过仓库ID获取指定仓库产品信息
        /// </summary>
        /// <param name="wareHouseID">仓库ID</param>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetMoreProductInfoByWareHouseID(int wareHouseID)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@wareHouseID",SqlDbType.Int)
            };
            sparams[0].Value = wareHouseID;
            return DBHelper.ExecuteDataTable("GetMoreProductInfoByWareHouseID", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 通过仓库ID，库位ID获取指定的仓库库位产品信息
        /// </summary>
        /// <param name="wareHouseID">仓库ID</param>
        /// <param name="depotSeatID">库位ID</param>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetMoreProductInfoByWareHouseIDDepotSeatID(int wareHouseID, int depotSeatID)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@wareHouseID",SqlDbType.Int),
                new SqlParameter("@depotSeatID",SqlDbType.Int)
            };
            sparams[0].Value = wareHouseID;
            sparams[1].Value = depotSeatID;
            return DBHelper.ExecuteDataTable("GetMoreProductInfoByWareHouseIDDepotSeatID", sparams, CommandType.StoredProcedure);
        }


        /// <summary>
        /// 通过产品编码联合查询获取指定产品所在仓库的产品信息
        /// Get more product information of the WareHouse by ProductCode 
        /// </summary>
        /// <param name="productCode">productCode</param>
        /// <returns>Return DataTable object</returns>
        public static DataTable GetMoreWareHouseProductInfoByProductCode(string productCode)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@productCode",SqlDbType.VarChar,100)                
            };
            sparams[0].Value = productCode;
            return DBHelper.ExecuteDataTable("GetMoreWareHouseProductInfoByProductCode", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 通过产品编码联合查询获取指定产品所在店铺里的产品信息
        /// Get more product information of the store by ProductCode
        /// </summary>
        /// <param name="productCode">productCode</param>
        /// <returns>Return DataTable object</returns>
        public static DataTable GetMoreStoreProductInfoByProductCode(string productCode)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@productCode",SqlDbType.VarChar,100)
            };
            sparams[0].Value = productCode;
            return DBHelper.ExecuteDataTable("GetMoreStoreProductInfoByProductCode", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 获取产品列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetProductList()
        {
            return DBHelper.ExecuteDataTable("GetProductList", CommandType.StoredProcedure);
        }

        /// <summary>
        /// 获取不包含组合产品的列表
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetNoIncluedCombineProductList()
        {
            return DBHelper.ExecuteDataTable("GetNoIncluedCombineProductList", CommandType.StoredProcedure);
        }

        #region 获取产品树
        /// <summary>
        /// 获取产品树
        /// </summary>
        /// <returns></returns>
        public DataTable GetProductTree(int countryID)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@countryID",SqlDbType.Int)
            };
            sparams[0].Value = countryID;
            return DBHelper.ExecuteDataTable("GetProductTree", sparams, CommandType.StoredProcedure);
        }
        #endregion
        /// <summary>
        /// 获取所有产品数据
        /// </summary>
        /// <param name="storeid">店铺的编号</param>
        /// <returns></returns>
        public static DataTable GetAllProducts(string storeid)
        {
            string country = DBHelper.ExecuteScalar("select substring(scpccode,1,2) from storeinfo where storeid=@storeid", new SqlParameter[] { new SqlParameter("@storeid", storeid) }, CommandType.Text).ToString();
            string ssQL = @"Select PID, isFold , ProductID , ProductName , CommonPrice , CommonPV , PreferentialPrice , PreferentialPV, 0 As num From Product WHERE IsSell=0 and countrycode=@cou ";
            return DBHelper.ExecuteDataTable(ssQL, new SqlParameter[] { new SqlParameter("@cou", country) }, CommandType.Text);
        }

        public static DataTable GetAllProducts1(string storeid, double huilv)
        {
            string country = DBHelper.ExecuteScalar("select substring(scpccode,1,2) from storeinfo where storeid=@storeid", new SqlParameter[] { new SqlParameter("@storeid", storeid) }, CommandType.Text).ToString();
            string ssQL = @"Select PID, isFold , ProductID , ProductName , CommonPrice*" + huilv + " as CommonPrice , CommonPV , PreferentialPrice *" + huilv + " as PreferentialPrice, PreferentialPV, 0 As num From Product WHERE IsSell=0 and countrycode=@cou ";
            return DBHelper.ExecuteDataTable(ssQL, new SqlParameter[] { new SqlParameter("@cou", country) }, CommandType.Text);
        }
        public static DataTable GetTotalProduct(string storeOrderID)
        {
            string ssQL = @"select p.ProductName,o.Price,o.Pv,p.BigSmallMultiple,pu.ProductUnitName,o.Quantity,o.NeedNumber
             from dbo.Product p inner join dbo.OrderDetail o on p.ProductID=o.ProductID inner join dbo.ProductUnit pu
            on P.BigProductUnitID=pu.ProductUnitID where o.StoreOrderID=@num";

            SqlParameter[] parm = new SqlParameter[] { 
              new SqlParameter("@num",SqlDbType.VarChar,50)
            
            };
            parm[0].Value = storeOrderID;

            return DBHelper.ExecuteDataTable(ssQL,parm,CommandType.Text);
        }
        /// <summary>
        /// 根据国家和库位获取产品信息
        /// </summary>
        /// <param name="Country"></param>
        /// <param name="DepotSeatID"></param>
        /// <returns></returns>
        public static DataTable GetProductQueryMenu(int Country, int WareHouseID, int DepotSeatID)
        {
            SqlParameter[] param = new SqlParameter[] { 
                new SqlParameter("@Country",Country),
                new SqlParameter("@WareHouseID",WareHouseID),
                new SqlParameter("@DepotSeatID",DepotSeatID)
            };
            return DBHelper.ExecuteDataTable("GetProductQueantityMenu", param, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 获取所有出售产品
        /// </summary>
        /// <returns>产品列表</returns>
        public static DataTable GetALLProduct()
        {
            string _sSQL = @"Select BigProductUnitID,SmallProductUnitID, BigSmallMultiple, PreferentialPrice ,
							 PreferentialPV , ProductID, ProductName ,PID,Weight,ProductType,CommonPrice 
                             From Product where IsSell=0";
            return DBHelper.ExecuteDataTable(_sSQL);
        }

        /// <summary>
        /// 根据物品编号获取类编号
        /// </summary>
        /// <param name="proid">物品编号</param>
        /// <returns>类编号</returns>
        public static string GetPid(string proid)
        {
            SqlParameter para = new SqlParameter("@proid", proid);
            return DBHelper.ExecuteScalar("select pid from product where productid=@proid", para, CommandType.Text).ToString();
        }

        /// <summary>
        /// 返回是否是组合产品
        /// </summary>
        /// <param name="productid">产品ID</param>
        /// <returns>是否</returns>
        public static bool GetIsCombine(int productid)
        {
            object obj = DBHelper.ExecuteScalar("select iscombineproduct from product where productid=@proid", new SqlParameter[] { new SqlParameter("@proid", productid) }, CommandType.Text);
            if (obj != null)
            {
                if (obj.ToString() == "1")
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 根据产品获取产品的币种
        /// </summary>
        /// <param name="proid">物品编号</param>
        /// <returns>类编号</returns>
        public static string GetPCurr(int proid)
        {
            SqlParameter para = new SqlParameter("@proid", proid);
            return DBHelper.ExecuteScalar("select Currency from dbo.Product where productid=@proid", para, CommandType.Text).ToString();
        }
        /// <summary>
        /// 根据国家和库位获取产品信息(不包含组合产品)
        /// </summary>
        /// <param name="Country"></param>
        /// <param name="DepotSeatID"></param>
        /// <returns></returns>
        public static DataTable GetProductEQueryMenu(int Country, int WareHouseID, int DepotSeatID)
        {
            SqlParameter[] param = new SqlParameter[] { 
                new SqlParameter("@Country",Country),
                new SqlParameter("@WareHouseID",WareHouseID),
                new SqlParameter("@DepotSeatID",DepotSeatID)
            };
            return DBHelper.ExecuteDataTable("GetProductEQueantityMenu", param, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 根据产品编号获取该产品是否销售
        /// </summary>
        /// <param name="proid"></param>
        /// <returns></returns>
        public static string GetIsSellByProId(int proid)
        { 
            return DBHelper.ExecuteScalar("select issell from product where productid=@proid",new SqlParameter[1]{new SqlParameter("@proid",proid)},CommandType.Text).ToString();
        }

        /// <summary>
        /// 获取商品集合
        /// </summary>
        /// <returns></returns>
        public static SqlDataReader GetProductInfo()
        {
            string strSql = "Select productID,productName,PreferentialPrice,PreferentialPV From Product Where IsSell=0";
            return DBHelper.ExecuteReader(strSql, CommandType.Text);
        }

        /// <summary>
        /// 获取产品ID通过名称模糊查询   ---DS2012
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns></returns>
        public static DataTable GetProductID(string name)
        {
            string strSql = "Select productID,productName From Product Where productname like '%" + name + "%' and isfold=1";
            return DBHelper.ExecuteDataTable(strSql);
            //return DBHelper.ExecuteDataTable(strSql, new SqlParameter[] { new SqlParameter("@name", name) }, CommandType.Text);
        }

        /// <summary>
        /// 获取产品ID通过名称查询   ---DS2012
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns></returns>
        public static DataTable GetPProductID(string PID)
        {
            string strSql = "Select productID From Product Where PID = @PID and isfold=1";
            return DBHelper.ExecuteDataTable(strSql, new SqlParameter[] { new SqlParameter("@PID", PID) }, CommandType.Text);
        }
    }
}
