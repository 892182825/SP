using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web.UI.WebControls;
using DAL;
using System.Data.SqlClient;
using System.Data;

namespace BLL.Registration_declarations
{

    public class DetialQueryBLL
    {
        /// <summary>
        /// 判断当前是否有已结算的工资
        /// </summary>
        /// <returns></returns>
        public static int GetMaxExpectNumByIsSuance()
        {
            return ReleaseDAL.GetMaxExpectNumByIsSuance();
        }
        /// <summary>
        /// 根据店铺编号获取店长编号
        /// </summary>
        /// <param name="StoreID">店铺编号</param>
        /// <returns></returns>
        public static string GetNumberByStoreID(string StoreID)
        {
            return ReleaseDAL.GetNumberByStoreID(StoreID);
        }
        /// <summary>
        /// 根据会员编号获取会员姓名
        /// </summary>
        /// <param name="Number">编号</param>
        /// <returns></returns>
        public static string GetNameByPlacement(string Number)
        {
            return ReleaseDAL.GetNameByPlacement(Number);
        }
        /// <summary>
        /// //根据编号、期数读取结算表信息
        /// </summary>
        /// <param name="ExpectNum">期数</param>
        /// <param name="Number">编号</param>
        /// <returns></returns>
        public static DataTable GetMemberInfoBalanceByNumber(int ExpectNum, string Number)
        {
            return ReleaseDAL.GetMemberInfoBalanceByNumber(ExpectNum, Number);
        }
        public static int IsSuanceJj(int ExpectNum)
        {
            return ReleaseDAL.ISSuanceJj(ExpectNum);
        }
    }
    /// <summary>
    /// 此类包含高级查询需要的信息
    /// </summary>
    public class QueryInfo
    {
        public QueryInfo()
        {

        }

        /// <summary>
        /// 高级查询
        /// </summary>
        /// <param name="rate"></param>
        /// <returns></returns>
        public static ArrayList getList(double rate)
        {
            TranslationBase tlb = new TranslationBase();

            ArrayList coll = new ArrayList();

            string columns = "";

            object obj = DBHelper.ExecuteScalar("select ColumnName from QueryColumnsInfo where Number='" + System.Web.HttpContext.Current.Session["Company"].ToString() + "'");
            if (obj != null)
            {
                columns = obj.ToString();
            }

            //添加会员基本信息表的字段
            coll.Add(new QueryKey("mi.Number", tlb.GetTran("000024", "会员编号"), true, "string", false, new Unit(100)));
            coll.Add(new QueryKey("mi.Placement", tlb.GetTran("000706", "安置人编号"), oldColumn(tlb.GetTran("000706", "安置人编号"), columns), "string", false, new Unit(100)));
            coll.Add(new QueryKey("mp.Name", tlb.GetTran("000097", "安置人姓名"), oldColumn(tlb.GetTran("000097", "安置人姓名"), columns), "text", false, new Unit(100)));
            coll.Add(new QueryKey("mi.Direct", tlb.GetTran("000043", "推荐人编号"), oldColumn(tlb.GetTran("000043", "推荐人编号"), columns), "string", false, new Unit(100)));
            coll.Add(new QueryKey("d.Name", tlb.GetTran("000192", "推荐人姓名"), oldColumn(tlb.GetTran("000192", "推荐人姓名"), columns), "text", false, new Unit(100)));
            coll.Add(new QueryKey("mi.Name", tlb.GetTran("000025", "会员姓名"), oldColumn(tlb.GetTran("000025", "会员姓名"), columns), "text", false, new Unit(100)));
            coll.Add(new QueryKey("mi.PetName", tlb.GetTran("000063", "会员昵称"), oldColumn(tlb.GetTran("000063", "会员昵称"), columns), "string", false, new Unit(100)));
            coll.Add(new QueryKey("lv.levelstr", tlb.GetTran("000903", "会员级别"), oldColumn(tlb.GetTran("000903", "会员级别"), columns), "string", false, new Unit(100)));
            coll.Add(new QueryKey("mi.RegisterDate", tlb.GetTran("000057", "注册日期"), oldColumn(tlb.GetTran("000057", "注册日期"), columns), "text", false, new Unit(100)));
            coll.Add(new QueryKey("dateadd(hh,8,mi.advtime)", tlb.GetTran("007301", "激活日期"), oldColumn(tlb.GetTran("007301", "激活日期"), columns), "text", false, new Unit(100)));
            coll.Add(new QueryKey("mi.Birthday", tlb.GetTran("000105", "出生日期"), oldColumn(tlb.GetTran("000105", "出生日期"), columns), "text", false, new Unit(100)));
            coll.Add(new QueryKey(@"mi.Sex", tlb.GetTran("000085", "性别"), oldColumn(tlb.GetTran("000085", "性别"), columns), "text", false, new Unit(100)));
            coll.Add(new QueryKey(@"mi.PostalCode", tlb.GetTran("000073", "邮编"), oldColumn(tlb.GetTran("000073", "邮编"), columns), "string", false, new Unit(100)));
            coll.Add(new QueryKey(@"mi.HomeTele", tlb.GetTran("000065", "家庭电话"), oldColumn(tlb.GetTran("000065", "家庭电话"), columns), "string", false, new Unit(100)));
            coll.Add(new QueryKey(@"mi.OfficeTele", tlb.GetTran("000044", "办公电话"), oldColumn(tlb.GetTran("000044", "办公电话"), columns), "string", false, new Unit(100)));
            coll.Add(new QueryKey(@"mi.MobileTele", tlb.GetTran("000069", "移动电话"), oldColumn(tlb.GetTran("000069", "移动电话"), columns), "string", false, new Unit(100)));
            coll.Add(new QueryKey(@"mi.FaxTele ", tlb.GetTran("000071", "传真电话"), oldColumn(tlb.GetTran("000071", "传真电话"), columns), "string", false, new Unit(100)));
            coll.Add(new QueryKey("mi.CPCCode", tlb.GetTran("000916", "国家省份城市"), oldColumn(tlb.GetTran("000916", "国家省份城市"), columns), "string", false, new Unit(100)));
            coll.Add(new QueryKey("mi.Address", tlb.GetTran("000920", "详细地址"), oldColumn(tlb.GetTran("000920", "详细地址"), columns), "text", false, new Unit(100)));
            coll.Add(new QueryKey("p.PaperType", tlb.GetTran("000081", "证件类型"), oldColumn(tlb.GetTran("000081", "证件类型"), columns), "text", false, new Unit(100)));
            coll.Add(new QueryKey(@"mi.PaperNumber", tlb.GetTran("000083", "证件号码"), oldColumn(tlb.GetTran("000083", "证件号码"), columns), "string", false, new Unit(100)));
            coll.Add(new QueryKey("mi.bankcode", tlb.GetTran("000087", "开户银行"), oldColumn(tlb.GetTran("000087", "开户银行"), columns), "text", false, new Unit(100)));
            coll.Add(new QueryKey("mi.bankBook", tlb.GetTran("000086", "开户名"), oldColumn(tlb.GetTran("000086", "开户名"), columns), "text", false, new Unit(100)));
            coll.Add(new QueryKey("mi.bankbranchname", tlb.GetTran("006046", "支行名称"), oldColumn(tlb.GetTran("006046", "支行名称"), columns), "text", false, new Unit(100)));
            coll.Add(new QueryKey(@"mi.BankBook ", tlb.GetTran("000111", "银行账号"), oldColumn(tlb.GetTran("000111", "银行账号"), columns), "string", false, new Unit(100)));
            coll.Add(new QueryKey(@"mi.BankCard ", tlb.GetTran("000923", "卡号"), oldColumn(tlb.GetTran("000923", "卡号"), columns), "string", false, new Unit(100)));
            coll.Add(new QueryKey("mi.ExpectNum", tlb.GetTran("000090", "个人期数"), oldColumn(tlb.GetTran("000090", "个人期数"), columns), "text", false, new Unit(100)));

            //添加会员结算表的字段
            //coll.Add(new QueryKey("mn.LayerBit1", tlb.GetTran("000928", "安置层位"), oldColumn(tlb.GetTran("000928", "安置层位"), columns), "text", false, "double", new Unit(100)));
            //coll.Add(new QueryKey("mn.LayerBit2", tlb.GetTran("000929", "推荐层位"), oldColumn(tlb.GetTran("000929", "推荐层位"), columns), "text", false, "double", new Unit(100)));
            //coll.Add(new QueryKey("mn.Ordinal1", tlb.GetTran("000930", "安置序号"), oldColumn(tlb.GetTran("000930", "安置序号"), columns), "text", false, "double", new Unit(100)));
            //coll.Add(new QueryKey("mn.Ordinal2", tlb.GetTran("000932", "推荐序号"), oldColumn(tlb.GetTran("000932", "推荐序号"), columns), "text", false, "double", new Unit(100)));
            coll.Add(new QueryKey("mn.TotalNetNum", tlb.GetTran("000933", "总网人数"), oldColumn(tlb.GetTran("000933", "总网人数"), columns), "text", true, "int", new Unit(100)));
            coll.Add(new QueryKey("mn.CurrentNewNetNum", tlb.GetTran("000934", "新网人数"), oldColumn(tlb.GetTran("000934", "新网人数"), columns), "text", true, "int", new Unit(100)));
            coll.Add(new QueryKey("mn.CurrentTotalNetRecord ", tlb.GetTran("000936", "新网分数"), oldColumn(tlb.GetTran("000936", "新网分数"), columns), "text", true, "double", new Unit(100)));
            coll.Add(new QueryKey("mn.TotalNetRecord", tlb.GetTran("000937", "总网分数"), oldColumn(tlb.GetTran("000937", "总网分数"), columns), "text", true, "double", new Unit(100)));
            coll.Add(new QueryKey("mn.CurrentOneMark ", tlb.GetTran("000939", "新个分数"), oldColumn(tlb.GetTran("000939", "新个分数"), columns), "text", true, "double", new Unit(100)));
            coll.Add(new QueryKey("mn.TotalOneMark ", tlb.GetTran("000940", "总个分数"), oldColumn(tlb.GetTran("000940", "总个分数"), columns), "text", true, "double", new Unit(100)));
            coll.Add(new QueryKey("mn.NotTotalMark ", tlb.GetTran("000942", "未付款的总个分数"), oldColumn(tlb.GetTran("000942", "未付款的总个分数"), columns), "text", true, "double", new Unit(100)));
            coll.Add(new QueryKey("mn.TotalNotNetRecord ", tlb.GetTran("000944", "未付款的总网分数"), oldColumn(tlb.GetTran("000944", "未付款的总网分数"), columns), "text", true, "double", new Unit(100)));
            //------------------------------------------------------------------------------------------------------------
            //coll.Add(new QueryKey("mn.Bonus0 * " + rate.ToString() + " ", tlb.GetTran("7577", "无"), oldColumn(tlb.GetTran("7577", "无"), columns), "text", true,"double", new Unit(100)));
            coll.Add(new QueryKey("mn.Bonus0 * " + rate.ToString() + " ", tlb.GetTran
("010002", "业绩奖"), oldColumn(tlb.GetTran
("010002", "业绩奖"), columns), "text", true, "double", new Unit(100)));
//            coll.Add(new QueryKey("mn.Bonus2 * " + rate.ToString() + " ", tlb.GetTran
//("007579", "回本奖"), oldColumn(tlb.GetTran
//("007579", "回本奖"), columns), "text", true, "double", new Unit(100)));
//            coll.Add(new QueryKey("mn.Bonus3 * " + rate.ToString() + " ", tlb.GetTran
//("007580", "大区奖"), oldColumn(tlb.GetTran
//("007580", "大区奖"), columns), "text", true, "double", new Unit(100)));
//            coll.Add(new QueryKey("mn.Bonus4 * " + rate.ToString() + " ", tlb.GetTran
//("007581", "小区奖"), oldColumn(tlb.GetTran
//("007581", "小区奖"), columns), "text", true, "double", new Unit(100)));
//            coll.Add(new QueryKey("mn.Bonus5 * " + rate.ToString() + " ", tlb.GetTran
//("007582", "永续奖"), oldColumn(tlb.GetTran
//("007582", "永续奖"), columns), "text", true, "double", new Unit(100)));

//            coll.Add(new QueryKey("mn.Bonus6 * " + rate.ToString() + " ", tlb.GetTran
//("009128", "进步奖"), oldColumn(tlb.GetTran
//("009128", "进步奖"), columns), "text", true, "double", new Unit(100)));
//            //------------------------------------------------------------------------------------------------------------
//            coll.Add(new QueryKey("mn.Kougl * " + rate.ToString() + " ", tlb.GetTran("001352", "网平台综合管理费"), oldColumn(tlb.GetTran("001352", "网平台综合管理费"), columns), "text", true, "double", new Unit(100)));
//            coll.Add(new QueryKey("mn.Koufl * " + rate.ToString() + " ", tlb.GetTran("001353", "网扣福利奖金"), oldColumn(tlb.GetTran("001353", "网扣福利奖金"), columns), "text", true, "double", new Unit(100)));
//            coll.Add(new QueryKey("mn.Koufx * " + rate.ToString() + " ", tlb.GetTran("001355", "网扣重复消费"), oldColumn(tlb.GetTran("001355", "网扣重复消费"), columns), "text", true, "double", new Unit(100)));

            coll.Add(new QueryKey("mn.CurrentTotalMoney * " + rate.ToString() + " ", tlb.GetTran("000247", "总计"), oldColumn(tlb.GetTran("000247", "总计"), columns), "text", true, "double", new Unit(100)));
            //coll.Add(new QueryKey("mn.DeductTax * " + rate.ToString() + " ", tlb.GetTran("000249", "扣税"), oldColumn(tlb.GetTran("000249", "扣税"), columns), "text", true, "double", new Unit(100)));
            //coll.Add(new QueryKey("mn.DeductMoney * " + rate.ToString() + " ", tlb.GetTran("000251", "扣款"), oldColumn(tlb.GetTran("000251", "扣款"), columns), "text", true, "double", new Unit(100)));
            coll.Add(new QueryKey("mn.CurrentSolidSend * " + rate.ToString() + " ", tlb.GetTran("000254", "实发"), oldColumn(tlb.GetTran("000254", "实发"), columns), "text", true, "double", new Unit(100)));
            //coll.Add(new QueryKey("mn.BonusAccumulation * " + rate.ToString() + " ", tlb.GetTran("000951", "总计累计"), oldColumn(tlb.GetTran("000951", "总计累计"), columns), "text", true, "double", new Unit(100)));
            //coll.Add(new QueryKey("mn.SolidSendAccumulation * " + rate.ToString() + " ", tlb.GetTran("000953", "实发累计"), oldColumn(tlb.GetTran("000953", "实发累计"), columns), "text", true, "double", new Unit(100)));
            coll.Add(new QueryKey("cfg.[Date]", tlb.GetTran("000954", "期数日期"), oldColumn(tlb.GetTran("000954", "期数日期"), columns), "text", true, "text", new Unit(100)));

            #region 删除不能显示的
            if (System.Web.HttpContext.Current.Session["dian"] != null)
            {
                Hashtable htb = new Hashtable();
                System.Data.SqlClient.SqlDataReader sdr = DBHelper.ExecuteReader("SELECT FieldExtend,StoreSelect FROM QueryField");
                while (sdr.Read())
                {
                    if (htb[sdr["FieldExtend"].ToString().Trim()] == null) htb.Add(sdr["FieldExtend"].ToString().Trim(), Convert.ToInt32(sdr["StoreSelect"]));
                }
                sdr.Close();
                for (int i = coll.Count - 1; i >= 0; i--)
                {
                    string key = ((QueryKey)coll[i]).Name.Trim();
                    if (htb[key] != null && (int)htb[key] == 0)
                    {
                        coll.RemoveAt(i);
                    }
                }
            }
            else
            {
                if (System.Web.HttpContext.Current.Session["bh"] != null)
                {
                    Hashtable htb = new Hashtable();
                    System.Data.SqlClient.SqlDataReader sdr = DBHelper.ExecuteReader("SELECT FieldExtend,MemberSelect FROM QueryField");
                    while (sdr.Read())
                    {
                        if (htb[sdr["FieldExtend"].ToString().Trim()] == null) htb.Add(sdr["FieldExtend"].ToString().Trim(), Convert.ToInt32(sdr["MemberSelect"]));
                    }
                    sdr.Close();
                    for (int i = coll.Count - 1; i >= 0; i--)
                    {
                        string key = ((QueryKey)coll[i]).Name.Trim();
                        if (htb[key] != null && (int)htb[key] == 0)
                        {
                            coll.RemoveAt(i);
                        }
                    }
                }
            }
            #endregion

            return coll;
        }
        public static bool oldColumn(string columnname, string columns)
        {
            //if (columns.IndexOf(columnname) != -1)
            //{
            //    return true;
            //}
            //else
            //{
            return false;
            //}

        }

        /// <summary>
        /// 删除高级查询的记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int deletequeryrecord(int id)
        {
            string sql = "delete from QueryColumnsInfo where id = " + id;
            int num = DAL.DBHelper.ExecuteNonQuery(sql);
            return num;
        }

        /// <summary>
        /// 判断是否存在记录
        /// </summary>
        /// <param name="ColumnName">存储列</param>
        /// <param name="Number">编号</param>
        /// <returns></returns>
        public static bool IsExist(string ColumnName, string Number)
        {
            string sql = "update QueryColumnsInfo set ColumnName=@ColumnName where Number=@Number";
            SqlParameter[] parm = new SqlParameter[] { 
                new SqlParameter("@ColumnName",SqlDbType.NVarChar,4000),
                new SqlParameter("@Number",SqlDbType.VarChar,50)
            };
            parm[0].Value = ColumnName;
            parm[1].Value = Number;
            int num = DBHelper.ExecuteNonQuery(sql, parm, CommandType.Text);
            if (num == 0)
            {
                sql = "";
                sql = "insert into QueryColumnsInfo (ColumnName,Number) values (@ColumnName,@Number)";
                SqlParameter[] parm1 = new SqlParameter[] { 
                    new SqlParameter("@ColumnName",SqlDbType.NVarChar,4000),
                    new SqlParameter("@Number",SqlDbType.VarChar,50)
                };
                parm1[0].Value = ColumnName;
                parm1[1].Value = Number;
                num += DBHelper.ExecuteNonQuery(sql, parm1, CommandType.Text);
            }
            if (num > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 判断是否存在记录
        /// </summary>
        /// <param name="ColumnName">存储列</param>
        /// <param name="Number">编号</param>
        /// <returns></returns>
        public static bool IsExist(string ColumnName, string sqlwhere, string orderby, string Number, string typename)
        {
            string sql = "insert into QueryColumnsInfo(ColumnName,Number,type,sqlwhere,orderby) values (@ColumnName,@Number,@type,@sqlwhere,@orderby)";
            SqlParameter[] parm1 = new SqlParameter[] { 
                new SqlParameter("@ColumnName",ColumnName),
                new SqlParameter("@Number",Number),
                new SqlParameter("@type",typename),
                new SqlParameter("@sqlwhere",sqlwhere),
                new SqlParameter("@orderby",orderby)
            };
            int num = DBHelper.ExecuteNonQuery(sql, parm1, CommandType.Text);
            if (num > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        /// <summary>
        /// 判断记录名是否存在
        /// </summary>
        /// <param name="typename"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool IsExistTypeName(string typename, string number)
        {
            string sql = "select count(*) from QueryColumnsInfo where number=@number and type=@typename";
            SqlParameter[] par = new SqlParameter[]{
                new SqlParameter("@number",number),
                new SqlParameter("@typename",typename)
            };
            object o = DBHelper.ExecuteScalar(sql, par, CommandType.Text);
            if (o != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 查询记录名
        /// </summary>
        /// <param name="typename"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public static DataTable QueryTypeName(string number)
        {
            string sql = "select id,type from QueryColumnsInfo where number=@number";
            SqlParameter[] par = new SqlParameter[]{
                new SqlParameter("@number",number)
            };
            return DBHelper.ExecuteDataTable(sql, par, CommandType.Text);
        }
        /// <summary>
        /// 查询记录
        /// </summary>
        /// <param name="typename"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public static DataTable QueryQueryColumnsInfo(string id)
        {
            string sql = "select * from QueryColumnsInfo where id=@id";
            SqlParameter[] par = new SqlParameter[]{
                new SqlParameter("@id",id)
            };
            return DBHelper.ExecuteDataTable(sql, par, CommandType.Text);
        }

        /// <summary>
        /// 根据期数和编号查询层位序号
        /// </summary>
        /// <param name="ExpectNum">期数</param>
        /// <param name="Number">编号</param>
        /// <returns></returns>
        public static void GetSerial(int ExpectNum, string Number, out int LayerBit1, out int Ordinal1)
        {
            LayerBit1 = 0;
            Ordinal1 = 0;
            string sql = "SELECT Ordinal1,LayerBit1 FROM MemberInfoBalance" + ExpectNum + " WHERE Number=@Number";
            SqlParameter[] parm = new SqlParameter[] { 
                new SqlParameter("@Number",SqlDbType.VarChar,50)
            };
            parm[0].Value = Number;
            SqlDataReader reader = DBHelper.ExecuteReader(sql, parm, CommandType.Text);
            if (reader.Read())
            {
                Ordinal1 = Convert.ToInt32(reader["Ordinal1"].ToString());
                LayerBit1 = Convert.ToInt32(reader["LayerBit1"].ToString());
            }
            reader.Close();
            reader.Dispose();
        }
        /// <summary>
        /// 获得最大序号
        /// </summary>
        /// <param name="ExpectNum">期数</param>
        /// <param name="LayerBit1">层位</param>
        /// <param name="Ordinal1">序号</param>
        /// <returns></returns>
        public static object GetMaxxuhao(int ExpectNum, int LayerBit1, int Ordinal1)
        {
            string sql = "SELECT min(Ordinal1) as maxOrdinal1 FROM MemberInfoBalance" + ExpectNum + " WHERE Ordinal1>@Ordinal1 and LayerBit1 <=@LayerBit1";
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Ordinal1",SqlDbType.Int),
                new SqlParameter("@LayerBit1",SqlDbType.Int)
            };
            parm[0].Value = Ordinal1;
            parm[1].Value = LayerBit1;
            object obj = DBHelper.ExecuteScalar(sql, parm, CommandType.Text);
            return obj;
        }
        public static bool IsNumberExist(string number, int ExpectNum)
        {
            string sql = "select count(1) from MemberInfoBalance" + ExpectNum + " WHERE number='" + number + "'";
            object obj = DBHelper.ExecuteScalar(sql);
            if (int.Parse(obj.ToString()) > 0)
            {
                return true;
            }
            else
                return false;
        }
        /// <summary>
        /// 返回组合查询信息
        /// </summary>
        /// <param name="sql">组合语句</param>
        /// <returns></returns>
        public static DataTable GetWeaveInfo(string sql)
        {
            return DBHelper.ExecuteDataTable(sql, CommandType.Text);
        }

        public static int GetCount(string a_count)
        {
            return Convert.ToInt32(DBHelper.ExecuteScalar(a_count));
        }
        public static int GetRecordCount(string sql)
        {
            return int.Parse(DBHelper.ExecuteScalar(sql).ToString());
        }
    }
    //一个查询条件的项
    public class QueryKey
    {
        private string key;					//键
        private string name;					//值
        private bool check;					//默认是否选中
        private string dispType;				//显示的模式，比如，可以显示文本或者显示超链接
        private bool needCount;				//表示是否需要结算
        private string countType;				//如果需要结算，则表示结算时使用的类型
        Unit width = new Unit(100);			//表示该

        /// <summary>
        /// 申明一个高级查询项
        /// </summary>
        /// <param name="key">要查询的数据表加上字段名，如  Table.Column</param>
        /// <param name="name">该列显示时的列名</param>
        /// <param name="check">默认是否选中该项</param>
        /// <param name="dispType">在列表中要显示的类型，如text表示按钮，link表示链接</param>
        /// <param name="needCount">表示该列如果包含在列表中，是否需要统计和</param>
        /// <param name="countType">表示统计和的时候需要使用的类型，如 int,double等,大小写必须匹配</param>
        /// <param name="width">表示该列显示的时候的宽度</param>
        public QueryKey(string key, string name, bool check, string dispType, bool needCount, string countType, Unit width)
        {
            this.key = key;
            this.name = name;
            this.check = check;
            this.dispType = dispType;
            this.needCount = needCount;
            this.countType = countType;
            this.width = width;
        }
        /// <summary>
        /// 申明一个高级查询项
        /// </summary>
        /// <param name="key">要查询的数据表加上字段名，如  Table.Column</param>
        /// <param name="name">该列显示时的列名</param>
        /// <param name="check">默认是否选中该项</param>
        /// <param name="dispType">在列表中要显示的类型，如text表示按钮，link表示链接</param>
        /// <param name="needCount">表示该列如果包含在列表中，是否需要统计和</param>
        /// <param name="width">表示该列显示的时候的宽度</param>
        public QueryKey(string key, string name, bool check, string dispType, bool needCount, Unit width)
        {
            this.key = key;
            this.name = name;
            this.check = check;
            this.dispType = dispType;
            this.needCount = needCount;
            this.width = width;
        }

        public string Key
        {
            get { return this.key; }
            set { this.key = value; }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public bool Check
        {
            get { return this.check; }
            set { this.check = value; }
        }

        public string DispType
        {
            get { return this.dispType; }
            set { this.dispType = value; }
        }

        public bool NeedCount
        {
            get { return this.needCount; }
            set { this.needCount = value; }
        }

        public string CountType
        {
            get { return this.countType; }
            set { this.countType = value; }
        }

        public Unit Width
        {
            get { return this.Width; }
            set { this.Width = value; }
        }
    }
}