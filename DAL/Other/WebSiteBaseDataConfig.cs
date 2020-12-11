using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Other;
using System.Data.SqlClient;

namespace DAL.Other
{
    public class WebSiteBaseDataConfig
    {
        private const string SqlSelect = "Select CurrentRegisterMode From Global_Config ";
        private const string SqlUpdate = "Update Global_Config Set CurrentRegisterMode = @CurrentRegisterMode ";

        private const string SqlSelect1 = "Select CurrentStoreMode From Global_Config ";
        private const string SqlUpdate1 = "Update Global_Config Set CurrentStoreMode = @CurrentStoreMode ";
        #region 得到网站的全局状态
        /// <summary>
        /// 		/// 得到网站的全局状态
        /// </summary>
        public static EnumRegisterMemberType GetCurrentRegisterMode()
        {
            int currentMode = (int)EnumRegisterMemberType.None;
            SqlDataReader reader = DBHelper.ExecuteReader(SqlSelect);
            if (reader.Read())
            {
                currentMode = Convert.ToInt32(reader[0]);
            }
            else
                currentMode = (int)EnumRegisterMemberType.None;

            reader.Close();

            return (EnumRegisterMemberType)currentMode;
        }
        #endregion

        #region 设置网站的全局状态，GlobalConfig中没有数据会自动填充，有则更新
        /// <summary>
        /// 设置网站的全局状态，GlobalConfig中没有数据会自动填充，有则更新
        /// </summary>
        public static int UpdateCurrentRegisterMode(EnumRegisterMemberType enumRegisterMode)
        {
            Model.Other.WebSiteBaseDataConfig.EnumRegisterMemberType = enumRegisterMode;
            string sSQL = @"IF Exists(Select Top 1 ID From GlobalConfig)
            						   	Update GlobalConfig Set CurrentRegisterMode =" + (int)Model.Other.WebSiteBaseDataConfig.EnumRegisterMemberType + @"			  
            						Else
            							Insert Into GlobalConfig(CurrentRegisterMode) Values (" + (int)Model.Other.WebSiteBaseDataConfig.EnumRegisterMemberType + @") ";
            return DBHelper.ExecuteNonQuery(sSQL);
        }
        #endregion



        #region 得到网站的全局状态
        /// <summary>
        /// 		/// 得到网站的全局状态
        /// </summary>
        public static EnumRegisterStoreType GetCurrentStoreMode()
        {
            int currentMode = (int)EnumRegisterStoreType.None;
            SqlDataReader reader = DBHelper.ExecuteReader(SqlSelect1);
            if (reader.Read())
            {
                currentMode = Convert.ToInt32(reader[0]);
            }
            else
                currentMode = (int)EnumRegisterStoreType.None;

            reader.Close();

            return (EnumRegisterStoreType)currentMode;
        }
        #endregion

        #region 设置网站的全局状态，GlobalConfig中没有数据会自动填充，有则更新
        /// <summary>
        /// 设置网站的全局状态，GlobalConfig中没有数据会自动填充，有则更新
        /// </summary>
        public static int UpdateCurrentStoreMode(EnumRegisterStoreType enumStoreMode)
        {
            Model.Other.WebSiteBaseDataConfig.EnumRegisterStoreType = enumStoreMode;
            string sSQL = @"IF Exists(Select Top 1 ID From Global_Config)
            						   	Update Global_Config Set CurrentStoreMode =" + (int)Model.Other.WebSiteBaseDataConfig.EnumRegisterStoreType + @"			  
            						Else
            							Insert Into Global_Config(CurrentStoreMode) Values (" + (int)Model.Other.WebSiteBaseDataConfig.EnumRegisterStoreType + @") ";
            return DBHelper.ExecuteNonQuery(sSQL);
        }
        #endregion
    }
}
