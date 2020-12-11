using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace BLL.other.Company
{
    public class SMSBLL
    {
        public SMSBLL()
        { 
        }

        /// <summary>
        /// 获取数据库中的预设短信SMSSendBLL
        /// </summary>
        /// <returns></returns>
        public DataTable GetPreSetSMS()
        {
            string ExSql = "select PresetMsg,id,categoryID,addDate from T_PresetSMS order by id desc";
            return DAL.DBHelper.ExecuteDataTable(ExSql);
        }

        /// <summary>
        /// 添加新的预设短信项
        /// </summary>
        /// <param name="PreSMS">预设短信内容</param>
        /// <param name="categoryID">类型别ID，目前未设计分类功能，扩展用</param>
        /// <returns></returns>
        public bool AddNewPreSetSMS(string PreSMS, int categoryID,out string msg)
        {
            msg = "";
            if (PreSMS == "")
            {
                msg = BLL.Translation.Translate("006831", "预设内容不能为空");
                return false;
            }
            string ExSql = string.Empty;
            int var = 0;
            SqlParameter[] spas = new SqlParameter[] { 
            new SqlParameter ("@PresetMsg",SqlDbType .NVarChar ,1000),
            new SqlParameter ("@categoryID",SqlDbType.Int)
            };
            spas[0].Value = PreSMS;
            spas[1].Value = categoryID;

            ExSql = "select count(1) from T_PresetSMS where PresetMsg=@PresetMsg";
            var = Convert.ToInt32 (DAL.DBHelper.ExecuteScalar(ExSql, spas, CommandType.Text));
            if (var > 0)
            {
                msg = BLL.Translation.Translate("006830", "此预设内容己存在");
                return false;
            }

            ExSql = "insert into T_PresetSMS(PresetMsg,categoryID,addDate) values(@PresetMsg,@categoryID,getdate())";
          
            var = DAL.DBHelper.ExecuteNonQuery(ExSql, spas, CommandType.Text);
            if (var > 0)
            {
                msg = BLL.Translation.Translate("000006", "添加成功");
                return true;
            }
            else
            {
                msg = BLL.Translation.Translate("000007", "添加失败");
                return false;
            }
        }

        /// <summary>
        ///修改预设短信
        /// </summary>
        /// <param name="PreSMS">预设短信内容</param>
        /// <param name="categoryID">类型别ID，目前未设计分类功能，扩展用</param>
        /// <returns></returns>
        public bool UpdatePreSetSMS(string PreSMS, int categoryID,int id,out string msg)
        { 
            msg = "";
            if (PreSMS == "")
            {
                msg = BLL.Translation.Translate("006831", "预设内容不能为空");                
                return false;
            }

          
            string ExSql=string .Empty ;
            int var = 0;
            SqlParameter[] spas = new SqlParameter[] { 
            new SqlParameter ("@PresetMsg",SqlDbType .NVarChar ,1000),
            new SqlParameter ("@categoryID",SqlDbType.Int),            
            new SqlParameter ("@id",SqlDbType.Int)
            };
            spas[0].Value = PreSMS;
            spas[1].Value = categoryID;
            spas[2].Value = id;

            ExSql = "select count(1) from T_PresetSMS where PresetMsg=@PresetMsg and  id!=@id";
            var = Convert.ToInt32 (DAL.DBHelper.ExecuteScalar (ExSql, spas, CommandType.Text));
            if (var > 0)
            {
                msg = BLL.Translation.Translate("006830", "此预设内容己存在");                
                return false;
            }
            ExSql = "update T_PresetSMS set PresetMsg=@PresetMsg,categoryID=@categoryID where id=@id";           
            var = DAL.DBHelper.ExecuteNonQuery(ExSql, spas, CommandType.Text);
            if (var > 0)
            {
                msg = BLL.Translation .Translate ("000001", "修改成功");
                return true;
            }
            else
            {
                msg = BLL.Translation.Translate("000002", "修改失败");
                return false;
            }
        }

        /// <summary>
        /// 删除预设短信项
        /// </summary>
        /// <param name="PreSMS">预设短信内容</param>
        /// <param name="categoryID">类型别ID，目前未设计分类功能，扩展用</param>
        /// <returns></returns>
        public bool DelPreSetSMS(int id)
        {
            string ExSql = "delete from T_PresetSMS  where id=@id";
            SqlParameter[] spas = new SqlParameter[] { 
                new SqlParameter ("@id",SqlDbType.Int)
            };
            spas[0].Value = id;          
            int var = DAL.DBHelper.ExecuteNonQuery(ExSql, spas, CommandType.Text);
            if (var > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 短信群发
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="sendMsg"></param>
        /// <param name="enumStatus"></param>
        /// <returns></returns>
        public static bool SendMsg(SqlTransaction tran, string sendMsg, int qishu,string condition,Model.EnumStatusModel enumStatus,out string outMsg )
        {
            string ExSql=string .Empty ;
            string tbName = string.Empty;//
            string columns=string .Empty ;
            string tableMIB=" MemberInfoBalance" + qishu;
            if (enumStatus == Model.EnumStatusModel.enum_StatusMember)
            {// 
                columns = "mi.MobileTele,mi.Number as No,mib.CurrentTotalMoney,mi.Name,mi.PetName as PetName";
                tbName = " MemberInfo mi inner join " + tableMIB + " mib on mi.Number=mib.Number ";
            }
            else if (enumStatus == Model.EnumStatusModel.enum_StatusStore)
            {// 
                columns = " si.MobileTele ,si.StoreID as No,si.Name,si.StoreName as PetName ";
                tbName = " StoreInfo si ";
            }
            ExSql = "select " + columns + " from " + tbName + " where " + condition;

            Regex rxNo = new Regex(@"(\[No\])", RegexOptions.IgnoreCase);
            Regex rxName = new Regex(@"(\[Name\])", RegexOptions.IgnoreCase);
            Regex rxPetName = new Regex(@"(\[PetName\])", RegexOptions.IgnoreCase);
            Regex rxMobile = new Regex(@"(1)\d{12}", RegexOptions.IgnoreCase);

            DataTable dt = DAL.DBHelper.ExecuteDataTable(tran, ExSql);
            string mobile = string.Empty;
            string No = string.Empty;
            string name = string.Empty;
            string petName = string.Empty;
            bool flag = false;
            int pointer = 0;
            string tempMsg = string.Empty;
            string outInfo = string.Empty;
            foreach (DataRow dr in dt.Rows)
            {
                mobile = dr["MobileTele"].ToString().Trim();                
                if (mobile != ""&&!rxMobile.IsMatch (mobile ))
                {
                    tempMsg = sendMsg;
                    No = dr["No"].ToString().Trim();
                    name = Encryption.Encryption .GetDecipherName(dr["Name"].ToString().Trim());
                    petName = Encryption.Encryption .GetDecipherName( dr["PetName"].ToString().Trim());
                    tempMsg = rxName.Replace(tempMsg, name);
                    tempMsg = rxPetName.Replace(tempMsg, petName);
                    tempMsg = rxNo.Replace(tempMsg, No);
                    flag = BLL.MobileSMS.SendMsgTo(tran, No, "", mobile, tempMsg, out outInfo, Model.SMSCategory.sms_GroupSend);
                    if (flag)
                        pointer++;
                }
            }
            if (pointer == 0)
            {
                outMsg = BLL.Translation.Translate("007143", "无接收对象或接收对象手机号码有误");
                flag = false;
            }
            else
            {
                outMsg = BLL.Translation.Translate("005615","发送成功");
                flag = true;
            }


            return flag;
            
        }

        /// <summary>
        /// 短信群发
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="sendMsg"></param>
        /// <param name="qishu"></param>
        /// <param name="condition"></param>
        /// <param name="enumStatus"></param>
        /// <param name="outMsg"></param>
        /// <param name="Dtable"></param>
        /// <returns></returns>
        public static bool SendMsg(SqlTransaction tran, string sendMsg, int qishu, string condition, Model.EnumStatusModel enumStatus, out string outMsg,int IsTrem,string number)
        {
            string ExSql = string.Empty;
            string tbName = string.Empty;//
            string columns = string.Empty;
            string tableMIB = " MemberInfoBalance" + qishu;
            DataTable dt = null;
            if (IsTrem == 0)
            {
                if (enumStatus == Model.EnumStatusModel.enum_StatusMember)
                {// 
                    columns = "mi.MobileTele,mi.Number as No,mib.CurrentTotalMoney,mi.Name,mi.PetName as PetName";
                    tbName = " MemberInfo mi inner join " + tableMIB + " mib on mi.Number=mib.Number ";
                }
                else if (enumStatus == Model.EnumStatusModel.enum_StatusStore)
                {// 
                    columns = " si.MobileTele ,si.StoreID as No,si.Name,si.StoreName as PetName ";
                    tbName = " StoreInfo si ";
                }
                ExSql = "select " + columns + " from " + tbName + " where " + condition;



                 dt = DAL.DBHelper.ExecuteDataTable(tran, ExSql);
            }
            else
            {
                SqlParameter[] par = new SqlParameter[] { 
                new SqlParameter("@number",number),
                new SqlParameter("@qishu",qishu),
                new SqlParameter("@type",IsTrem)
                };
                dt = DAL.DBHelper.ExecuteDataTable(tran, "GetTremMobile", par, CommandType.StoredProcedure);
            }

            Regex rxNo = new Regex(@"(\[No\])", RegexOptions.IgnoreCase);
            Regex rxName = new Regex(@"(\[Name\])", RegexOptions.IgnoreCase);
            Regex rxPetName = new Regex(@"(\[PetName\])", RegexOptions.IgnoreCase);
            Regex rxMobile = new Regex(@"(1)\d{12}", RegexOptions.IgnoreCase);


            string mobile = string.Empty;
            string No = string.Empty;
            string name = string.Empty;
            string petName = string.Empty;
            bool flag = false;
            int pointer = 0;
            string tempMsg = string.Empty;
            string outInfo = string.Empty;
            foreach (DataRow dr in dt.Rows)
            {
                mobile = dr["MobileTele"].ToString().Trim();
                if (mobile != "" && !rxMobile.IsMatch(mobile))
                {
                    tempMsg = sendMsg;
                    No = dr["No"].ToString().Trim();
                    name = Encryption.Encryption.GetDecipherName(dr["Name"].ToString().Trim());
                    petName = Encryption.Encryption.GetDecipherName(dr["PetName"].ToString().Trim());
                    tempMsg = rxName.Replace(tempMsg, name);
                    tempMsg = rxPetName.Replace(tempMsg, petName);
                    tempMsg = rxNo.Replace(tempMsg, No);
                    flag = BLL.MobileSMS.SendMsgTo(tran, No, "", mobile, tempMsg, out outInfo, Model.SMSCategory.sms_GroupSend);
                    if (flag)
                        pointer++;
                }
            }
            if (pointer == 0)
            {
                outMsg = BLL.Translation.Translate("007143", "无接收对象或接收对象手机号码有误");
                flag = false;
            }
            else
            {
                outMsg = BLL.Translation.Translate("005615", "发送成功");
                flag = true;
            }


            return flag;

        }


    }
}
